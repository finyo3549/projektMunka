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
        Schema::create('userboosts', function (Blueprint $table) {
            $table->id();
            $table->boolean('booster1');
            $table->boolean('booster2');
            $table->boolean('booster3');
            $table->timestamps();
        });
        Schema::table('userboosts', function(Blueprint $table){
            $table->foreign('id')->references('id')->on('players');
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('userboost');
    }
};
