<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Userboost extends Model
{
    use HasFactory;

    protected $fillable = ['used','user_id','booster_id'];
    // Kapcsolat a User modellhez
    public function user() {
        return $this->belongsTo(User::class, 'user_id');
    }

    // Kapcsolat a Booster modellhez
    public function booster() {
        return $this->belongsTo(Booster::class, 'booster_id');
    }
}
