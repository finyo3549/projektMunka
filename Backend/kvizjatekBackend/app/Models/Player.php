<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

    //--------------
    //Ez szerintem így már feleslegessé vállt
    //--------------
class Player extends Model
{
    use HasFactory;

    protected $fillable = ['name', 'password', 'email', 'credit', 'is_active'];
}
