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
        $booster_names = ['Telefonhívás', 'Közönség','Felező', ];
        $booster_description = ['A telefonhívás segítségével egy barátját hívhatja fel a játékos, aki segítséget nyújthat a válaszhoz. A barát nem minden esetben tudja a jó választ', 'A közönség segítségével a játékos megtekintheti, hogy a közönség melyik válaszlehetőséget támogatja. Néha a közönség is tévedhet...' ,'A felező segítségével két rossz válaszlehetőséget elvesz a gép, így már csak kettőből kell választania a játékosnak'];

        foreach (array_combine($booster_names, $booster_description) as $name => $description) {
            Booster::create([
                'booster_name' => $name,
                'booster_description' => $description
            ]);
        }
    }
}
