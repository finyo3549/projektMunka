<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Topic;
use App\Models\Question;

class TopicController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        return Topic::all();
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        $topic = Topic::create($request->all());
        return response()->json($topic, 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $topic = Topic::find($id);
        if (is_null($topic)) {
            return response()->json(['message' => "Topic not found with id: $id"], 404);
        } else {
            return response()->json(['message' => $topic, 200]);}

    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        $topic = Topic::find($id);
        if (is_null($topic)) {
            return response()->json(['message' => "Topic not found with id: $id"], 404);
        } else {
            $topic->fill($request->all());
            $topic->save();
            return response()->json($topic, 200);
        }
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $topic = Topic::find($id);
        if (is_null($topic)) {
            return response()->json(['message' => "Topic not found with id: $id"], 404);
        } else {
            $questionExists = Question::where('topic_id', $id)->exists();
            if($questionExists){
                return response()->json(['message' => "Topic cannot be deleted because it has questions associated with it"], 400);
            } else {
                $topic->delete();
                return response()->noContent();
            }
    }
}
}
