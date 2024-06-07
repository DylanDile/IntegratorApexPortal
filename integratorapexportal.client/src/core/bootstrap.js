/* eslint-disable no-unused-vars */

// Load Axios into window

import axios from 'axios';

window.axios = axios;

window.axios.defaults.baseURL = import.meta.env.ASPNETCORE_AXIOS_BASEURL;

window.axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
window.axios.defaults.headers.common['Accept'] = 'application/json';

window.axios.interceptors.response.use(
    function(response) { return response;},
    function(error) {
        let returnError =  Promise.reject(error);

        if (!error.response)
        {
            // network error

            window.Swal.fire({
                title: "Network Error",
                text: "Page will automatically refresh",
                icon: "warning",
                showConfirmButton: true,
                padding: '20px',
                confirmButtonColor: '#673c7a',
            }).then((res) => {
                // window.location.reload();
                window.location.href = window.location.href;
            });

        }

        if (error.response)
        {
            // Login Redirect

            if (401 === error.response.status)
            {
                window.Swal.fire({
                    title: "Session Expired",
                    text: "Your session has expired. You will be redirected to login",
                    icon: "warning",
                    showConfirmButton: true,
                    padding: '20px',
                    confirmButtonColor: '#673c7a',
                }).then((res) => {
                    window.location = '/login';
                });
            }

            if (403 === error.response.status)
            {
                window.Swal.fire({
                    text: "Your action was Forbidden",
                    icon: 'error',
                    showConfirmButton: true,
                    padding: '20px',
                    confirmButtonColor: '#673c7a',
                }).then(r => {});
            }

            // Server Error

            if (500 === error.response.status)
            {
                window.Swal.fire({
                    icon: 'error',
                    showConfirmButton: true,
                    padding: '20px',
                    confirmButtonColor: '#673c7a',
                    text: "An Error occurred , Kindly Contact Admin if it persists",
                }).then(r => {});
            }
        }

        return returnError;
    });