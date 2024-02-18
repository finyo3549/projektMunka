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
    public function store(Request $request)
    {
        $question = Question::create([
            'questiontext' => $request->question,
            'topicid' => $request->answer,
            'answer1' => $request->distractor1,
            'answer2' => $request->distractor2,
            'answer3' => $request->distractor3,
            'category' => $request->category,
            'difficulty' => $request->difficulty
        ]);
        return response()->json($question, 201);
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
}
