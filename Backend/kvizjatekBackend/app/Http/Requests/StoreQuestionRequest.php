<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreQuestionRequest extends FormRequest
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
            'question_text' => 'required|string|max:255',
            'topic_id' => 'required|integer|exists:topics,id',
            'answers' => 'required|array|min:1',
            'answers.*.answer_text' => 'required|string|max:255',
            'answers.*.is_correct' => 'required|boolean'
        ];
    }
    public function messages()
    {
        return [
            'question_text.required' => 'A kérdés szövegét kötelező megadni.',
            'topic_id.required' => 'A témakör azonosítóját kötelező megadni.',
            'topic_id.exists' => 'A megadott témakör nem létezik.',
            'answers.required' => 'Legalább egy választ meg kell adni.',
            'answers.array' => 'A válaszoknak tömbnek kell lenniük.',
            'answers.*.answer_text.required' => 'Minden válasznak kell lennie szövegének.',
            'answers.*.is_correct.required' => 'Minden válaszhoz meg kell adni, hogy helyes-e.'
        ];
    }
}
