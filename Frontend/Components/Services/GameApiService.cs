﻿using PicasYFamas.BlazorApp.Components.Models;

namespace PicasYFamas.BlazorApp.Components.Services
{
    public class GameApiService : IGameApiService
    {
        private readonly HttpClient _httpClient;

        public GameApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/game/v1/register", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<RegisterPlayerResponse>>();
                    return result?.Data;
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<ApiResponse<ErrorResponse>>();
                    throw new Exception(error?.Data?.Message ?? "Error al registrar jugador");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de conexión: {ex.Message}");
            }
        }

        public async Task<StartGameResponse> StartGameAsync(StartGameRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/game/v1/start", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<StartGameResponse>>();
                    return result?.Data;
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<ApiResponse<ErrorResponse>>();
                    throw new Exception(error?.Data?.Message ?? "Error al iniciar juego");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de conexión: {ex.Message}");
            }
        }

        public async Task<GuessNumberResponse> GuessNumberAsync(GuessNumberRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/game/v1/guess", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<GuessNumberResponse>>();
                    return result?.Data;
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<ApiResponse<ErrorResponse>>();
                    throw new Exception(error?.Data?.Message ?? "Error al procesar intento");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de conexión: {ex.Message}");
            }
        }

        public async Task<DashboardData> GetDashboardDataAsync()
        {
            try
            {
                // Obtener datos de los diferentes endpoints del dashboard
                var statsTask = _httpClient.GetFromJsonAsync<ApiResponse<DashboardStats>>("api/dashboard/v1/stats");
                var usersTask = _httpClient.GetFromJsonAsync<ApiResponse<List<UserRegistrationByDay>>>("api/dashboard/v1/users-per-day?days=7");
                var top5Task = _httpClient.GetFromJsonAsync<ApiResponse<List<TopGame>>>("api/dashboard/v1/top5-games");
                var gamesTask = _httpClient.GetFromJsonAsync<ApiResponse<List<GameAttempts>>>("api/dashboard/v1/games-attempts");

                await Task.WhenAll(statsTask, usersTask, top5Task, gamesTask);

                return new DashboardData
                {
                    Stats = statsTask.Result?.Data ?? new DashboardStats(),
                    UsersPerDay = usersTask.Result?.Data ?? new List<UserRegistrationByDay>(),
                    Top5Games = top5Task.Result?.Data ?? new List<TopGame>(),
                    AttemptsPerGame = gamesTask.Result?.Data ?? new List<GameAttempts>()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos del dashboard: {ex.Message}");
            }
        }
    }
}