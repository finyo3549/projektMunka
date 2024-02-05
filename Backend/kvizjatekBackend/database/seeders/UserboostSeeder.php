<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Userboost;
use \App\Models\Player;
use \App\Models\Rank;

class UserboostSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {

        Userboost::factory()
            ->count(15)

            ->create();
    }
}
