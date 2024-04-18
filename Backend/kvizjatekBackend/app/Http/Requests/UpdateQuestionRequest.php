<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateQuestionRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules()
    {
        return [
            'question_text' => 'sometimes|string|max:255',
            'topic_id' => 'sometimes|integer|exists:topics,id',
            'answers' => 'sometimes|array|min:1',
            'answers.*.id' => 'required_with:answers|exists:answers,id',
            'answers.*.answer_text' => 'sometimes|required_with:answers|string|max:255',
            'answers.*.is_correct' => 'sometimes|required_with:answers|boolean'
        ];
    }
    

    public function messages()
    {
        return [
            'question_text.sometimes' => 'A kérdés szövege opcionális, de ha megadod, akkor annak szövegnek kell lennie.',
            'topic_id.sometimes' => 'A témakör azonosítója opcionális, de ha megadod, annak létező témakörnek kell lennie.',
            'topic_id.integer' => 'A témakör azonosítójának egész számnak kell lennie.',
            'topic_id.exists' => 'A megadott témakör azonosító nem található az adatbázisban.',
            'answers.sometimes' => 'A válaszok megadása opcionális, de ha megadod, tömb formátumban kell elküldeni.',
            'answers.min' => 'Legalább egy választ meg kell adni, ha válaszokat szeretnél frissíteni vagy hozzáadni.',
            'answers.*.id.required_with' => 'Minden válasznak kell tartalmaznia egy azonosítót, ha válaszokat adtál meg.',
            'answers.*.id.exists' => 'A megadott válasz azonosító nem található az adatbázisban.',
            'answers.*.answer_text.sometimes' => 'A válasz szövege opcionális, de ha megadod, annak szövegnek kell lennie.',
            'answers.*.answer_text.required_with' => 'A válasz szövegét kötelező megadni, ha válaszokat szeretnél frissíteni vagy hozzáadni.',
            'answers.*.answer_text.max' => 'A válasz szövege nem lehet hosszabb 255 karakternél.',
            'answers.*.is_correct.sometimes' => 'A válasz helyességének jelölése opcionális, de ha megadod, annak logikai értéknek (igaz/hamis) kell lennie.',
            'answers.*.is_correct.required_with' => 'A válasz helyességét kötelező megadni, ha válaszokat szeretnél frissíteni vagy hozzáadni.',
            'answers.*.is_correct.boolean' => 'A válasz helyessége csak igaz vagy hamis lehet.'
        ];
    }
    
}
