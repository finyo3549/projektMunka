<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Question;

class QuestionController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Question::all();
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store()
    {

    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $question = Question::find($id);
        if(is_null ($question)){
            return response()->json(['message' => "Question not found with id: $id"], 404);
        } else {
            return response()->json($question, 200);
        }
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        $question = Question::find($id);
        if(is_null ($question)){
            return response()->json(['message' => "Question not found with id: $id"], 404);
    } else {
        $question->fill($request->all());
        $question->save();
        return response()->json($question, 200);
    }
}

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $question = Question::find($id);
        if(is_null ($question)){
            return response()->json(['message' => "Question not found with id: $id"], 404);
    } else {
        $question->delete();
        return response()->json(['message' => "Question deleted successfully"], 200);
    }
    }

    public function getQuestionsWithAnswers()
    {
        $questions = Question::with('answers')->get();
        return response()->json($questions);
    }
}
