<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Question extends Model
{
    use HasFactory;

    protected $fillable = ['question_text','topic_id',];
    /**
     * Egy kérdéshez tartozó válaszok relációja.
     */
    public function answers()
    {
        return $this->hasMany(Answer::class, 'question_id');
    }
}
