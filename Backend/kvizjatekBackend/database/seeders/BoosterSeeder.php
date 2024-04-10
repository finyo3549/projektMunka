<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use \App\Models\Booster;

class BoosterSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run()
    {
        $booster_names = ['felező', 'telefonhívás', 'közönség'];
        $booster_description = ['A felező segítségével két válaszlehetőség közül választhat a játékos.', 'A telefonhívás segítségével egy barátját hívhatja fel a játékos, aki segítséget nyújthat a válaszhoz.', 'A közönség segítségével a játékos megtekintheti, hogy a közönség melyik válaszlehetőséget támogatja.'];

        foreach (array_combine($booster_names, $booster_description) as $name => $description) {
            Booster::create([
                'booster_name' => $name,
                'booster_description' => $description
            ]);
        }
    }
}
