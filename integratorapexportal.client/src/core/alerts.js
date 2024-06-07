/* eslint-disable no-unused-vars */
import Swal from 'sweetalert2';
window.Swal = Swal;
let alerts = {
    success: function (response) {
        return new Promise((resolve, reject) => {
            window.Swal.fire({
                icon: 'success',
                text: response.data.message,
                showConfirmButton: true,
                confirmButtonColor: '#673c7a',
                padding: '20px',
            }).then((e) => {
                resolve(response);
            });
        });
    },
    warning: function (response) {
        return new Promise((resolve, reject) => {
            window.Swal.fire({
                icon: 'warning',
                title: response.data.message,
                showConfirmButton: true,
                confirmButtonColor: '#673c7a',
                padding: '20px',
            }).then((e) => {
                resolve(response);
            });
        });
    },
    error: function (response) {
        return new Promise((resolve, reject) => {
            window.Swal.fire({
                icon: 'error',
                text: response.data.message,
                showConfirmButton: true,
                confirmButtonColor: '#673c7a',
                padding: '20px',
            }).then((e) => {
                resolve(response);
            });
        });
    },
    formError: function (response) {
        return new Promise((resolve, reject) => {
            window.Swal.fire({
                icon: 'error',
                title: response.message,
                showConfirmButton: false,
                padding: '20px',
                timer: 2000
            }).then((e) => {
                resolve(response);
            });
        });
    }
};

window.alerts = alerts;
