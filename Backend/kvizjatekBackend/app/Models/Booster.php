<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Booster extends Model
{
    use HasFactory;
    protected $fillable = ['booster_name', 'reset_on_new_game'];
}
