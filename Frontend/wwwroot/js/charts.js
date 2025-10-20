// charts.js - Funciones para renderizar gráficos con Chart.js

let usersChart, topGamesChart, distributionChart;

window.renderCharts = function (usersData, topGamesData, distributionData) {
    // Destruir gráficos existentes
    if (usersChart) usersChart.destroy();
    if (topGamesChart) topGamesChart.destroy();
    if (distributionChart) distributionChart.destroy();

    // Gráfico de Usuarios por Día (Línea)
    const usersCtx = document.getElementById('usersChart');
    if (usersCtx) {
        usersChart = new Chart(usersCtx, {
            type: 'line',
            data: usersData,
            options: {
                responsive: true,
                maintainAspectRatio: true,
                plugins: {
                    legend: {
                        display: true,
                        position: 'top'
                    },
                    title: {
                        display: false
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1
                        }
                    }
                }
            }
        });
    }

    // Gráfico Top 5 Juegos (Barras Horizontales)
    const topGamesCtx = document.getElementById('topGamesChart');
    if (topGamesCtx) {
        topGamesChart = new Chart(topGamesCtx, {
            type: 'bar',
            data: topGamesData,
            options: {
                indexAxis: 'y',
                responsive: true,
                maintainAspectRatio: true,
                plugins: {
                    legend: {
                        display: false
                    }
                },
                scales: {
                    x: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1
                        }
                    }
                }
            }
        });
    }

    // Gráfico de Distribución (Dona)
    const distributionCtx = document.getElementById('distributionChart');
    if (distributionCtx) {
        distributionChart = new Chart(distributionCtx, {
            type: 'doughnut',
            data: distributionData,
            options: {
                responsive: true,
                maintainAspectRatio: true,
                plugins: {
                    legend: {
                        position: 'bottom'
                    }
                }
            }
        });
    }
};

// Función para actualizar datos en tiempo real (opcional)
window.updateChartData = function (chartId, newData) {
    let chart;
    switch (chartId) {
        case 'usersChart':
            chart = usersChart;
            break;
        case 'topGamesChart':
            chart = topGamesChart;
            break;
        case 'distributionChart':
            chart = distributionChart;
            break;
    }

    if (chart) {
        chart.data = newData;
        chart.update();
    }
};