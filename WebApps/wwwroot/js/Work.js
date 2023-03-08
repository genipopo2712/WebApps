function loadWorks(func) {
    $.post('/dashboard/work', (d) => {
        for (var i in d) {
            $(todoList).append(`
                <li>
                    <!-- drag handle -->
                    <span class="handle">
                        <i class="fas fa-ellipsis-v"></i>
                        <i class="fas fa-ellipsis-v"></i>
                    </span>
                    <!-- checkbox -->
                    <div  class="icheck-primary d-inline ml-2">
                                <input type="checkbox" name="todo${d[i].workId}" value="${d[i].workId}" id="todoCheck${d[i].workId}" ${d[i].checked ? 'checked' : ''}>
                                        <label for="todoCheck${d[i].workId}"></label>
                    </div>
                    <!-- todo text -->
                    <span class="text">${d[i].workName}</span>
                    <!-- Emphasis label -->
                    <small class="badge ${d[i].className}"><i class="far fa-clock"></i> ${d[i].ago} ${d[i].timeName}</small>
                    <!-- General tools such as edit or delete-->
                    <div class="tools">
                        <i class="fas fa-edit"></i>
                        <i class="fas fa-trash-o"></i>
                    </div>
                </li>
            `)
        }
        func();
    });
}