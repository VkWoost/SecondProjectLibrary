function Book() {
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
            columns: [
                { field: "Name", title: "Name" },
                { field: "YearOfPublication", title: "Year Of Publication" },
                { field: "Author.Name", title: "Author" },
                { field: "PublicationHouses", title: "Publication Houses", template: pHNames },
                ]
        });
    });
}

function pHNames(options) {
    if (options.PublicationHouses) {
        return options.PublicationHouses.map(x => x.Name).join(",");
    }
}