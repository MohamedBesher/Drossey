   $(function(){
           
$('.close_msg').click(function() {
        $(".notification_msg").remove();
    });
          //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
      
            //var MyObject = JSON.parse(data);
            
   
                    $(data).each(function(i,json){  
                        
                    if(json.text.title != undefined  && json.text.inside_title != undefined && json.text.pageHeader != undefined)
                        {
                             $('#title').append('<h4>' + json.text.title + '</h4><hr/><h5>'+json.text.inside_title+'</h5>');
                         $('#lesson_title').append(json.text.pageHeader);
                            
                        }
                    
                        
                        $("#noti_msg").append(json.text.prompt);
                          $('#results').append('<p>' + json.text.body +'<p>');
                       
                        $(json.images).each(function(i,item){
                            
                            
                       $("#allimages").append("<div class='item'><img src="+item.img+" width='100%'></div>");
                        $(".carousel-indicators").append('<li data-target="#myCarousel" data-slide-to="'+i+'"></li>');    
                        });
                            
                            $("#allimages div:first").addClass("active");
                            $(".carousel-indicators li:first").addClass("active");

                    
                });
         };
       
   
});
function loadScript(path, callback) {
 
    var done = false;
    var scr = document.createElement('script');
    scr.onload = handleLoad;
    scr.onreadystatechange = handleReadyStateChange;
    scr.onerror = handleError;
    scr.src = path;
    document.body.appendChild(scr);

    function handleLoad() {
        if (!done) {
            done = true;
            callback(path, "ok");
        }
    }

    function handleReadyStateChange() {
        var state;
        if (!done) {
            state = scr.readyState;
            if (state === "complete") {
                handleLoad();
            }
        }
    }

    function handleError() {
        if (!done) {
            done = true;
            callback(path, "error");
        }
    }
    return scr;
}