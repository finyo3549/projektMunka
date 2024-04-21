<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Http\Requests\TopicRequest;
use Illuminate\Http\Request;
use App\Models\Topic;
use App\Models\Question;

class TopicController extends Controller
{
    /**
     * Display a listing of topics.
     */
    public function index()
    {
        return Topic::all();
    }

    /**
     * Store a newly created topic in storage.
     */
    public function store(TopicRequest $request)
    {
        $topic = Topic::create([
            'topicname' => $request->topicname
        ]);
        return response()->json(['message' => 'Téma sikeresen létrehozva!', 'topic' => $topic], 201);
    }
    
    /**
     * Display the specified topic.
     */
    public function show(string $id)
    {
        $topic = Topic::find($id);
        if (!$topic) {
            return response()->json(['message' => "Téma nem található az azonosítóval: $id"], 404);
        }
        return response()->json(['topic' => $topic], 200);
    }

    /**
     * Update the specified topic in storage.
     */
    public function update(TopicRequest $request, string $id)
    {
        $topic = Topic::find($id);
        if (!$topic) {
            return response()->json(['message' => "Téma nem található az azonosítóval: $id"], 404);
        }
        $topic->update([
            'topicname' => $request->topicname
        ]);
        return response()->json(['message' => 'Téma sikeresen frissítve!', 'topic' => $topic], 200);
    }

    /**
     * Remove the topic from storage.
     */
    public function destroy(string $id)
    {
        $topic = Topic::find($id);
        if (!$topic) {
            return response()->json(['message' => "Téma nem található az azonosítóval: $id"], 404);
        }
        if (Question::where('topic_id', $id)->exists()) {
            return response()->json(['message' => "A témát nem lehet törölni, mert hozzá tartoznak kérdések."], 400);
        }
        $topic->delete();
        return response()->json(['message' => 'Téma sikeresen törölve!'], 200);
    }
}
