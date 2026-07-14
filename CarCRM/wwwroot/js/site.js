
$(function () {
    document.querySelectorAll('table.js-datatable').forEach(function (tabela) {
        new DataTable(tabela, {
            responsive: true,
            language: {

                url: 'https://cdn.datatables.net/plug-ins/2.3.8/i18n/pt-BR.json'
            },

            layout: {
                topStart: 'pageLength',   // seletor "itens por página"
                topEnd: 'search',         // busca GLOBAL (todas as colunas)
                bottomStart: 'info',
                bottomEnd: 'paging'
            },
            columnDefs: [
                // coluna(s) marcadas com a classe "no-sort" não ordenam nem entram na busca
                { targets: 'no-sort', orderable: false, searchable: false }
            ]
            /* FILTRO POR COLUNA (rodapé) - desativado temporariamente, revisar depois
            ,
            initComplete: function () {
                this.api().columns().every(function () {
                    let column = this;
                    let title = column.footer();
                    if (!title) return;

                    if (title.classList.contains('no-sort')) return;

                        let select = document.createElement('select');
                    select.className = 'form-select form-select-sm';
                    select.innerHTML = '<option value="">Todos</option>';
                    title.replaceChildren(select);

                    select.addEventListener('change', function () {
                        let val = select.value;
                            column.search(val ? '^' + val + '$' : '', true, false ).draw();
                        });

                        // Add list of options
                    column.data().unique().sort().each(function (d) {
                        if (d !== null && d !== '') {
                            select.add(new Option(d));
                        }

                    });
               });
            }
            */
        });
    });
});