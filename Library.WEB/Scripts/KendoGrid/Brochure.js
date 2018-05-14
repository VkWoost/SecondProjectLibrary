function Brochure() {
    $(document).ready(function () {
        dataSource = new kendo.data.DataSource({
            transport:
                {
                    read: function (e) {
                        $.ajax({
                            url: '/Brochure/GetBrochures',
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
                        TypeOfCover: { validation: { required: true } },
                        NumberOfPages: { validation: { required: true } }
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
                { field: "TypeOfCover", title: "Type Of Cover" },
                { field: "NumberOfPages", title: "Number Of Pages" },
                ],
        });
    });
}
