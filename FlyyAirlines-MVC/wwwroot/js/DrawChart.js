var ctx = document.getElementById('reservations').getContext('2d');
var chart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June'],
        datasets: [
            {
                label: 'Reservations in mounth',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgb(20, 30, 32)',
                    'rgb(240, 30, 32)',
                    'rgb(20, 250, 32)',
                    'rgb(20, 30, 232)',
                    'rgb(0, 252, 250)',
                    'rgb(60, 60, 60)',
                ],
                borderColor: 'rgba(255, 99, 32, 0.2)',
            },
        ],
    },
    options: {
        scales: {
            yAxes: [
                {
                    ticks: {
                        beginAtZero: true,
                    },
                },
            ],
        },
    }
})
