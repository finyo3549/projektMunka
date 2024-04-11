<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class RegisterRequest extends FormRequest
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
    public function rules(): array
    {
        return [
            'name' => "required|string",
            'email' => "required|email|unique:users,email",
            'password' => "required|string|min:8|regex:/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/",
            'gender' => 'string'
        ];
    }
    public function messages()
{
    return [
        'password.regex' => 'A jelszó minimum 8 karakter hosszú, és tartalmazzon legalább egy kisbetűt, egy nagybetűt, egy számot és egy speciális karaktert.'
    ];
}
}
