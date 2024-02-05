<?php

namespace Database\Factories;

use App\Models\Player;
use App\Models\Rank;
use Illuminate\Database\Eloquent\Factories\Factory;

class RankFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $userIds = Player::all()->pluck('id')->toArray();


        return [
            'score' => $this->faker->randomNumber(),
            'user_id' => fake()->unique()->randomElement($userIds),
        ];
    }
}
