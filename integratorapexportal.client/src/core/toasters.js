import {toast} from "vue3-toastify";

window.toastr = toast;
let toasters = {
    success: function (message) {
        return new Promise((resolve, reject) => {
            window.toastr.success(message);
            resolve();
        });
    },
    warning: function (message) {
        return new Promise((resolve, reject) => {
            window.toastr.warning(message,
                {
                    position: toast.POSITION.TOP_CENTER,
                });
            resolve();
        });
    },
    error: function (message) {
        return new Promise((resolve, reject) => {
            window.toastr.error(message,
                {
                    position: toast.POSITION.TOP_CENTER,
                });
            resolve();
        });
    },
    info: function (message) {
        return new Promise((resolve, reject) => {
            window.toastr.info(message,
                {
                    position: toast.POSITION.TOP_CENTER,
                });
            resolve();
        });
    }
};

window.toasters = toasters;
