
function drawDailyRevenueChart(labels, values, counts) {
    const ctx = document.getElementById('dailyRevenueChart').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Doanh thu ngày',
                data: values,
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            let revenue = context.formattedValue;
                            let index = context.dataIndex;
                            let count = counts[index]; // ✅ lấy số hóa đơn tương ứng
                            return `Doanh thu: ${revenue} ₫\nSố hóa đơn: ${count}`;
                        }
                    }
                }
            }
        }
    });
}


window.drawWeeklyRevenueChart = (labels, data) => {
    const ctx = document.getElementById('weeklyRevenueChart').getContext('2d');
    if (window.weeklyChart) window.weeklyChart.destroy();
    window.weeklyChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Doanh thu tuần',
                data: data,
                borderColor: 'rgba(255, 159, 64, 1)',
                backgroundColor: 'rgba(255, 159, 64, 0.3)',
                fill: true,
                tension: 0.3
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: v => v.toLocaleString() + ' đ'
                    }
                }
            }
        }
    });
};

window.drawMonthlyRevenueChart = (labels, data) => {
    const ctx = document.getElementById('monthlyRevenueChart').getContext('2d');
    if (window.monthlyChart) window.monthlyChart.destroy();
    window.monthlyChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Doanh thu tháng',
                data: data,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.3)',
                fill: true,
                tension: 0.3
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: v => v.toLocaleString() + ' đ'
                    }
                }
            }
        }
    });
};
window.drawBarChart = (canvasId, chartRefName, title, labels, data) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    if (!ctx) return;

    if (window[chartRefName]) {
        window[chartRefName].destroy();
    }

    window[chartRefName] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: title,
                data: data,
                backgroundColor: [
                    '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: value => value.toLocaleString() + ' sản phẩm'
                    }
                }
            }
        }
    });
};
