document.getElementById('taskInput').addEventListener('keypress', function
    (e) {
    if (e.key === "Enter") {
        document.getElementById('taskForm').submit();
    }
});