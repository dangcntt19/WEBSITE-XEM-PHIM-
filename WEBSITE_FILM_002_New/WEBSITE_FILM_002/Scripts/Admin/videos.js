$(document).ready(function () {
    $.ajax({
        url: "/Admin/VIDEOS/Get_Videos",
        type: 'POST',
        success: function (data) {
            let table_body = $("#table_videos tfoot");
            if (table_body) {
                const listVideos = JSON.parse(data);
                let DOM_ = "";
                $.each(listVideos, (I, e) => {
                    let item = `<tr>
                    <td>${e["videoname"]}</td>
                    <td  class="text-center">${e["datecreate"]}</td>
                    <td  class="text-center">${e["username"]}</td>
                    <td><a href="./DeleteVideo/${e["videoid"]}" class="nav-link text-danger"><i class="fas fa-trash"></i></a></td>
                </tr>`
                    DOM_ += item;
                })
                //console.log(DOM_);
                table_body.append(DOM_);
            }
        }
    })
})