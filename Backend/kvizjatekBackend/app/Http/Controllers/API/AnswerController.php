<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Answer;

class AnswerController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Answer::all();
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        $validatedData = $request->validate([
            'question_id' => 'required|exists:questions,id',
        ]);

        $questionExists = Question::where('id', $validatedData['question_id'])->exists();

        if (!$questionExists) {
            return response()->json(['error' => 'Question does not exist'], 404);
        }

        $answer = Answer::create($validatedData);

        return response()->json($answer, 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $answer = Answer::find($id);
        if (is_null($answer)) {
            return response()->json(['message' => "Answer not found with id: $id"], 404);
        } else {
            return response()->json(['message' => $answer, 200);}
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        $answer = Answer::find($id);
        if (is_null($answer)) {
            return response()->json(['message' => "Answer not found with id: $id"], 404);
        } else {
            $answer->fill($request->all());
            $answer->save();
            return response()->json($answer, 200);
        }
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $answer = Answer::find($id);
        if (is_null($answer)) {
            return response()->json(['message' => "Answer not found with id: $id"], 404);
        } else {
            $answer->delete();
            return response()->json(null, 204);
        }
    }
}
