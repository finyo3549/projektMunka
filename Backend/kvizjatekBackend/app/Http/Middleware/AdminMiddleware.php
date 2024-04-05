<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;

class AdminMiddleware
{
    /**
     * Handle an incoming request.
     *
     * @param  \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response)  $next
     */
    public function handle(Request $request, Closure $next)
    {
        // Ellenőrizzük, hogy a felhasználó be van-e jelentkezve és admin-e
        if (!auth()->check() || !auth()->user()->is_admin) {
            return response()->json(['message' => 'Access denied. Only admins can perform this action.'], 403);
        }

        return $next($request);
    }
}
