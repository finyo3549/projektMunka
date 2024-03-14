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
            $table->bigInteger('userid')->unsigned();
            $table->bigInteger('boosterid')->unsigned();
            $table->integer('quantity');

            $table->timestamps();
        });
        Schema::table('userboosts', function(Blueprint $table){
            $table->foreign('userid')->references('id')->on('users');
        });
        Schema::table('userboosts', function(Blueprint $table){
            $table->foreign('boosterid')->references('id')->on('boosters');
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
