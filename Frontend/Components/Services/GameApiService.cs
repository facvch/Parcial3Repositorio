using PicasYFamas.BlazorApp.Components.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PicasYFamas.BlazorApp.Components.Services
{
    public interface IGameApiService
    {
        Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request);
        Task<StartGameResponse> StartGameAsync(StartGameRequest request);
        Task<GuessNumberResponse> GuessNumberAsync(GuessNumberRequest request);
        Task<DashboardData> GetDashboardDataAsync();
    }

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
                // DashboardController usa ControllerBase, devuelve datos sin wrapper
                var statsTask = _httpClient.GetFromJsonAsync<DashboardStats>("api/dashboard/v1/stats");
                var usersTask = _httpClient.GetFromJsonAsync<List<UserRegistrationByDay>>("api/dashboard/v1/users-per-day?days=7");
                var top5Task = _httpClient.GetFromJsonAsync<List<TopGame>>("api/dashboard/v1/top5-games");
                var gamesTask = _httpClient.GetFromJsonAsync<List<GameAttempts>>("api/dashboard/v1/games-attempts");

                await Task.WhenAll(statsTask, usersTask, top5Task, gamesTask);

                return new DashboardData
                {
                    Stats = statsTask.Result ?? new DashboardStats(),
                    UsersPerDay = usersTask.Result ?? new List<UserRegistrationByDay>(),
                    Top5Games = top5Task.Result ?? new List<TopGame>(),
                    AttemptsPerGame = gamesTask.Result ?? new List<GameAttempts>()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos del dashboard: {ex.Message}");
            }
        }
    }
}