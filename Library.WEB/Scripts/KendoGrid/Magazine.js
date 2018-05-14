function Magazine() {
    $(document).ready(function () {
        dataSource = new kendo.data.DataSource({
            transport:
                {
                    read: function (e) {
                        $.ajax({
                            url: '/Magazine/GetMagazines',
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
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false, nullable: true },
                        Name: { validation: { required: true } },
                        Number: { validation: { required: true } },
                        YearOfPublication: { validation: { required: true } }
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
                { field: "Number", title: "Number" },
                { field: "YearOfPublication", title: "Year Of Publication" },
                ],
        });
    });
}