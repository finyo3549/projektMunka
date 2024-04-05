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
        // Létrehozás vagy módosítás
        Schema::create('userboosts', function (Blueprint $table) {
            $table->id(); // Egyedi azonosító a rekordnak
            $table->bigInteger('user_id')->unsigned();
            $table->bigInteger('booster_id')->unsigned();
            $table->boolean('used')->default(false); // Felhasználták-e a boostert
            $table->foreign('user_id')->references('id')->on('users');
            $table->foreign('booster_id')->references('id')->on('boosters');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('userboosts');
    }
};
