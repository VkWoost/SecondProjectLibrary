$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    url: '/Book/GetBooks',
                    type: "GET",
                    dataType: "json",
                    success: function (result) {
                        e.success(result);
                    },
                    error: function (result) {
                        e.error(result);
                    }
                });
            },
            create: function (options) {
                debugger;
                $.ajax({
                    url: '/Book/AddBook',
                    type: "POST",
                    dataType: "json",
                    data: {
                        bookViewModel: options.data
                    },
                    success: function (result) {
                        options.success(result);
                    },
                    error: function (result) {
                        options.error(result);
                    }
                });
            },
            update: function (opt) {
                $.ajax({
                    url: '/Book/BookEdit',
                    type: "POST",
                    dataType: "json",
                    data: {
                        bookViewModel: opt.data
                    },
                    success: function (result) {
                        opt.success(result);
                    },
                    error: function (result) {
                        opt.error(result);
                    },
                });
            },
            destroy: function (options) {
                $.ajax({
                    url: '/Book/DeleteBook/' + options.data.Id,
                    type: "POST",
                    dataType: "json",
                    success: function (result) {
                        options.success(result);
                    },
                    error: function (result) {
                        options.error(result);
                    }
                });
            },

        },
        batch: false,
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: true },
                    AuthorId: {},
                    Name: { validation: { required: true } },
                    YearOfPublication: { validation: { required: true } },
                    Author: { validation: { required: true } },
                    PublicationHouses: { validation: { required: true } }
                }
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        toolbar: ["create"],
        columns: [
            { field: "Name", title: "Name" },
            { field: "YearOfPublication", title: "Year Of Publication" },
            { field: "Author.Name", title: "Author", editor: authorEditor },
            { field: "PublicationHouses", title: "Publication Houses", editor: phEditor, template: pHNames },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "popup",
        edit: function (e) {
            if (e.model.isNew()) {
                $(".k-window-title")[0].innerHTML = "Add";
                $(".k-button.k-button-icontext.k-primary.k-grid-update")[0].textContent = "Add";
            }
        }
    });
});
function authorEditor(container, options) {
    $('<input name="AuthorId">').appendTo(container)
        .kendoDropDownList({
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        url: '/Author/GetAuthors',
                    }
                }
            }),
            dataTextField: "Name",
            dataValueField: "Id"
        });
}
function phEditor(container, options) {
    $('<input name="PublicationHouses">').appendTo(container)
        .kendoMultiSelect({
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        url: '/PublicationHouse/GetPublicationHouses',
                    }
                }
            }),
            dataTextField: "Name",
            dataValueField: "Id",
        });
}
function pHNames(options) {
    if (options.PublicationHouses) {
        return options.PublicationHouses.map(x => x.Name).join(",");
    }
}