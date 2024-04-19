<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Http\Requests\StoreQuestionRequest;
use App\Http\Requests\UpdateQuestionRequest;
use App\Models\Question;
use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Log;

class QuestionController extends Controller
{
 /**
     * Display a listing of all questions with their answers.
     */
    public function index()
    {
        $questions = Question::with('answers')->get();
        return response()->json($questions);
    }

    /**
     * Display a specific question with its answers.
     */
    public function show($id)
    {
        $question = Question::with('answers')->find($id);
        if (!$question) {
            return response()->json(['message' => "Question not found with id: $id"], 404);
        }
        return response()->json($question);
    }

    /**
     * Store a newly created answer in storage.
     */
    public function store(StoreQuestionRequest $request)
    {
    
        try {
            $validated = $request->validated();
        
            $question = Question::create([
                'question_text' => $validated['question_text'],
                'topic_id' => $validated['topic_id']
            ]);
        
            foreach ($validated['answers'] as $answerData) {
                $question->answers()->create($answerData);
            }
    
            $createdQuestion = $question->load('answers');
        
            return response()->json($createdQuestion, 201);
        } catch (\Exception $e) {
            return response()->json(['message' => 'Internal Server Error:' . $e->getMessage()], 500);
        }
    }
    

/**
     * Update a specific question and its answers.
     */
    public function update(UpdateQuestionRequest $request, $id): JsonResponse
    {
        $question = Question::with('answers')->find($id);

        if (!$question) {
            return response()->json(['message' => 'Question not found'], 404);
        }

        $question->update($request->only(['question_text', 'topic_id']));

        if ($request->has('answers')) {
            foreach ($request->input('answers') as $answerData) {
                $answer = $question->answers()->find($answerData['id']);

                if ($answer) {
                    $answer->update([
                        'answer_text' => $answerData['answer_text'],
                        'is_correct' => $answerData['is_correct']
                    ]);
                } else {
                    return response()->json(['message' => "Answer not found with id: {$answerData['id']}"], 404);
                }
            }
        }

        return response()->json($question->load('answers'), 200);
    }


    /**
     * Remove the specified question and its answers from storage.
     */
    public function destroy(string $id)
    {
        $question = Question::find($id);
        if (is_null($question)) {
            return response()->json(['message' => "Question not found with id: $id"], 404);
        } else {
            $deleteCount = $question->answers()->delete();
    
            $question->delete();
    
            return response()->json(['message' => "Question and answers deleted successfully"], 200);
        }
    }
    
}
