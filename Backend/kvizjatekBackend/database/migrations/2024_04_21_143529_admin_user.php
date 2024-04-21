<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        DB::table('users')->insert([
            'name' => 'admin',
            'email' => 'admin@petrik.hu',
            'password' => Hash::make('petrikAdmin1234!_'),
            'is_admin' => true,
            'created_at' => now(),
            'updated_at' => now()
        ]);
        DB::table('ranks')->insert([
            'user_id' => 1,
            'score' => 0,
            'created_at' => now(),
            'updated_at' => now()
        ]);
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        //
    }
};
