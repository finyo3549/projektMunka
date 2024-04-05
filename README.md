# projektMunka

# Projekt Ág Változások

Ez a dokumentum a `backend-BIGupdate` ágon történt változásokat írja le.

## Új Funkciók

- Funkció 1: Controllerek módosítása a játékban előjött szükséges információkhoz
- Funkció 2: Controller módosításokhoz végpontok definiálása az api.php-ban

## Javítások

- Javítás 1: Minden lehetséges helyen átírtam a neveket, hogy a "snake case" (kígyó eset) -et használjuk 
		Okok: 	-Homogenitás: Használtuk így is úgy is , szerintem ezért szólnának
			-A Laravel is ezt használja -> potenciális hibák elkerülése

		Hátrányok: -Ha már meglévő Frontend kódokat kell átírni, 
			    akkor sok esetben ez időigényesebb, mert minden hivatkozást át kell írni.

- Javítás 2: Kivettem nem használt  metódusokat a controllerekbebn, definiáljuk őket, ha szükség lesz rájuk

## Megjegyzés és további fejlesztések

-Frontend: A tesztelésekből kimaradtak a rank hívások és avatar feltöltése6módosítása
-Problémák: 	Szerver oldalon a tesztelés során elakdatam, mert nagyon sok kérésemet visszautasította 403-as kóddal.
		Valamiért nem engedélyezett módosításokat, gondolom nem volt hozzá jogosultságom.
-Fejlesztésre vár: 	Meg kellene oldani az admin hozzáféréseket, legalább szerver oldali tesztelés során működjön, mert így nem 			tudtam tovább haladni.
			Meg kellene beszélni, hogy melyik végpontokat fedje admin követelés és valahol azt is be lehetne állítani, hogy 			admin vagy az a user, aki megfelelő id-val vagy tokennel rendelkezik, pl.: A saját felhasználó módosíthassa az 			adatait,de más ne, viszont az admin is törölhesse a felhasználót vagy lekérhesse a rank adatait.

2024.04.05.
Gábor
