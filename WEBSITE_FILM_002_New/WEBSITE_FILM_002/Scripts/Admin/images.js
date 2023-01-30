$(document).ready(function() {
    $.ajax({
        url: "/Admin/IMAGES/ReturnImages",
        type: 'POST',
        success: function(data) {
            let table_body = $("#table_images tfoot");
            const listImages = JSON.parse(data);
            let DOM_ = "";
            $.each(listImages, (i, e) => {
                let item = `<tr>
                    <td>${e["IMAGENAME"]}</td>
                    <td  class="text-center">${e["DATECREATE"]}</td>
                    <td  class="text-center">${e["USERNAME"]}</td>
                    <td  class="text-center"><a href="./DeleteImage/${e["IMAGEID"]}" class="text-danger"><i class="fas fa-trash"></i></a></td>
                </tr>`
                DOM_ += item;
            })
            //console.log(DOM_);
            table_body.append(DOM_);
        }
    })
})