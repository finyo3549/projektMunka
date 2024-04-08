<?php

use GuzzleHttp\Middleware;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\API\QuestionController;
use App\Http\Controllers\API\TopicController;
use App\Http\Controllers\API\UserController;
use App\Http\Controllers\API\AuthController;
use App\Http\Controllers\API\RankController;
use App\Http\Controllers\API\AnswerController;

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
Route::post('/register', [AuthController::class, 'register']);
Route::post('/login', [AuthController::class, 'login']);
Route::post('/logout', [AuthController::class, 'logout'])->middleware('auth:sanctum');

//Route::put('/userboosts/use', [UserBoostController::class, 'updateUserBoosterStatus']);
//Route::post('/userboosts/reset/{user_id}', [UserBoostController::class, 'resetBoostersForNewGame']);
//Route::get('/userboosts/user/{user_id}', [UserBoostController::class, 'boostersByUserId']);
//Route::post('/userboosts/addBoosters/{user_id}', [UserBoostController::class, 'addMultipleBoosters']);
//Route::get('/questionsWithAnswers', [QuestionController::class, 'getQuestionsWithAnswers']);

//Route::apiResource('/booster',BoosterController::class);
Route::apiResource('/topics', TopicController::class);
//Route::apiResource('/userboosts', UserBoostController::class);
Route::apiResource('user-ranks', RankController::class); // ide nem rakni authot, mert a user-rank elérhető kell legyen login nékül is
Route::post('/user-ranks-reset', [RankController::class,'reset'])->middleware('auth:sanctum');
Route::apiResource('/questions', QuestionController::class);
Route::apiResource('/users', UserController::class)->middleware('auth:sanctum');
Route::apiResource('/answers', AnswerController::class);
Route::post('/users/inactivate/{id}', [UserController::class, 'inactivate'])->middleware('auth:sanctum');

// Létrehozás: Szimbolikus link a `public/storage` és `storage/app/public` között. Képek elérése: `yourdomain.com/storage/avatars/your_image.jpg`
// Parancs: `php artisan storage:link`
