<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use \App\Models\Player;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Booster>
 */
class BoosterFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */

     public function definition(): array
     {
         $boosters = [
             ['boostername' => 'felező', 'credit' => 100],
             ['boostername' => 'telefonhívás', 'credit' => 200],
             ['boostername' => 'közönség', 'credit' => 300]
         ];

         $randomBoosterIndex = $this->faker->unique()->randomElement([0, 1, 2]);

         return [
             'id' => $randomBoosterIndex + 1,
             'boostername' => $boosters[$randomBoosterIndex]['boostername'],
             'credit' => $boosters[$randomBoosterIndex]['credit'],
         ];
     }

    }

