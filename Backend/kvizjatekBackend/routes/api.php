<?php

use App\Http\Controllers\API\PlayerController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\API\QuestionController;
use App\Http\Controllers\API\TopicController;
use App\Http\Controllers\API\BoosterController;
use App\Http\Controllers\API\UserBoostController;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});
Route::apiResource('/booster',BoosterController::class);
Route::apiResource('/questions', QuestionController::class);
Route::apiResource('/topics', TopicController::class);
Route::apiResource('/userboosts',UserBoostController::class);
