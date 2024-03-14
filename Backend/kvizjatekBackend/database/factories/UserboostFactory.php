<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use \App\Models\User;
use \App\Models\Booster;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Userboost>
 */
class UserboostFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $userIds = User::all()->pluck('id')->toArray();
        $boosterIds = Booster::all()->pluck('id')->toArray();

        return [
            'userid'=> $this->faker->unique()->numberBetween(1, count($userIds)),
            'boosterid'=>$this->faker->numberBetween(1, count($boosterIds)),
            'quantity'=>fake()->numberBetween(1,3)
        ];
    }
}
