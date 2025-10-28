// charts.js - Funciones para renderizar gráficos con Chart.js

let usersChart, topGamesChart, distributionChart;

// Esperar a que Chart esté disponible
function waitForChart(callback) {
    if (typeof Chart !== 'undefined') {
        callback();
    } else {
        console.log('Esperando a que Chart.js se cargue...');
        setTimeout(() => waitForChart(callback), 100);
    }
}

window.renderCharts = function (usersData, topGamesData, distributionData) {
    console.log('=== INICIO renderCharts ===');
    console.log('Datos recibidos:', { usersData, topGamesData, distributionData });

    waitForChart(() => {
        try {
            // Destruir gráficos existentes
            if (usersChart) {
                console.log('Destruyendo usersChart existente');
                usersChart.destroy();
                usersChart = null;
            }
            if (topGamesChart) {
                console.log('Destruyendo topGamesChart existente');
                topGamesChart.destroy();
                topGamesChart = null;
            }
            if (distributionChart) {
                console.log('Destruyendo distributionChart existente');
                distributionChart.destroy();
                distributionChart = null;
            }

            // Esperar un poco para que el DOM esté listo
            setTimeout(() => {
                // Gráfico de Usuarios por Día (Línea)
                const usersCtx = document.getElementById('usersChart');
                console.log('Canvas usersChart:', usersCtx);

                if (usersCtx) {
                    console.log('Creando gráfico de usuarios...');
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
                    console.log('✓ Gráfico de usuarios creado');
                } else {
                    console.error('✗ Canvas usersChart NO encontrado en el DOM');
                }

                // Gráfico Top 5 Juegos (Barras Horizontales)
                const topGamesCtx = document.getElementById('topGamesChart');
                console.log('Canvas topGamesChart:', topGamesCtx);

                if (topGamesCtx) {
                    console.log('Creando gráfico top 5...');
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
                    console.log('✓ Gráfico top 5 creado');
                } else {
                    console.error('✗ Canvas topGamesChart NO encontrado en el DOM');
                }

                // Gráfico de Distribución (Dona)
                const distributionCtx = document.getElementById('distributionChart');
                console.log('Canvas distributionChart:', distributionCtx);

                if (distributionCtx) {
                    console.log('Creando gráfico de distribución...');
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
                    console.log('✓ Gráfico de distribución creado');
                } else {
                    console.error('✗ Canvas distributionChart NO encontrado en el DOM');
                }

                console.log('=== FIN renderCharts ===');
            }, 100);

        } catch (error) {
            console.error('❌ Error al renderizar gráficos:', error);
        }
    });
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
        console.log(`Gráfico ${chartId} actualizado`);
    }
};

console.log('✓ charts.js cargado correctamente');