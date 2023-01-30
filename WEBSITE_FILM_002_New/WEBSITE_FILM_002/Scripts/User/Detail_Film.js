$(document).ready(function() {
    let Film_Hot_Items = $("#film_hot_items");

    $.ajax({
        type: "POST",
        url: "../../_FILMS/Get_Film_Take_10",
        success: function(data) {
            // console.log("data", data);
            const _Json = JSON.parse(data)
            let _Dom = "";
            $.each(_Json, function(index, value) {
                if (index === 0) {
                    _Dom += `<div class="carousel-item active"><div class="row">`
                } else if (index == 4) {
                    _Dom += ` </div></div><div class="carousel-item"><div class="row">`
                } else if (index == 8) {
                    _Dom += ` </div></div><div class="carousel-item"><div class="row">`
                }
                _Dom += `<div class="col-6 col-md-6 col-lg-3 d-flex justify-content-center">
                            <div class=" card-hot-film" style="width: 8.7rem;">
                            <a href="./${value.filmid}">
                                    <img src="/Images/FilmPoster/${value.images}" class="" alt="${value.images}">
                                    <div class="">
                                        <h4 class="limit-text-2-line text-capitalize">${value.filmname.toLowerCase()}</h4>
                                    </div>
                                </div>
                            </a>
                        </div>`
                if (index == 11) {
                    _Dom += " </div></div>";
                }
            })
            Film_Hot_Items.append(_Dom);
        }
    })

    // rating 
    $("#sentReview").on('click', (e) => {
        e.preventDefault();

        let mark = ($('input[name=star]:checked').val() ? $('input[name=star]:checked').val() : 0)
        let result = window.location.href.split('/').reverse()[0]

        $.ajax({
            type: 'post',
            url: '../../_FILMS/Rating',
            data: {
                id: result,
                rating: mark
            },
            success: function(data) {
                alert("Cảm ơn bạn đã đóng góp\nChúc một ngày vui vẻ")
                console.log("Tang rating");
            }
        })
    })

    let url = window.location.href.split('/').reverse()[1]
    let id = window.location.href.split('/').reverse()[0]

    let DetailString = "Detail_Film";

    let PlayVdeowString = "PlayVideo";

    if (url == DetailString) {
        $.ajax({
            type: 'POST',
            url: "../../_FILMS/NewView",
            data: {
                id: id,
            },
            success: function(data) {
                console.log("Tang View");
            }
        })
    }


    // console.log("Detail", url == DetailString)
    // console.log("PlayVideo", url == PlayVdeowString)

})