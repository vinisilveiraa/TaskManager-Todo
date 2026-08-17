import { ref } from "vue";

const toast = ref(null);

export function useToast() {
    function show(type, title, message, duration = 3000) {
        toast.value = {
            type, title, message, duration
        };
    }

    function success(title, message) {
        show("success", title, message);
    }

    function error(title, message) {
        show("error", title, message);
    }

    function warning(title, message) {
        show("warning", title, message);
    }

    function info(title, message) {
        show("info", title, message);
    }

    function close() {
        toast.value = null;
    }

    return {
        toast,
        show,
        success,
        error,
        warning,
        info,
        close
    };
}