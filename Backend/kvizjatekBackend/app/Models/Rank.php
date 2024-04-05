<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Rank extends Model
{
    use HasFactory;
    protected $fillable = ['user_id', 'score'];
    protected $primaryKey = 'user_id';
    public $incrementing = false;
    public function user() {
        return $this->hasOne(User::class);
    }

}
