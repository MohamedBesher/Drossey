var index =0;
 var type="";
$(document).ready(function() {

           $('.audio_player').acornMediaPlayer({
               tooltipsOn:false
           });
           
           //load lesson json file.
            var source=sessionStorage.getItem('activity_src');
            var pagetype=sessionStorage.getItem('type');
            drawpage(pagetype,source);

                
        });
        
    
function drawpage(pagetype,Page) {
           
            // page content
             Pageheight=parseInt($(window).height())-60;
            if(pagetype == "activity")
              {
                  //use <object data=""> to load html code and run it . 
                  $('.site_contents').html($('<div class="video_cont"><object data="'+Page+'" style="width:100%;  min-height:'+Pageheight+'px"></object></div>'))
                  // Save data to sessionStorage
                //check if html has json file
                 if(Page.jsonSource !="")
                    sessionStorage.setItem('jsonFile', Page.jsonSource);
 
              }
         
          // Video
            else
            {
                loadScript(Page+".js",function (a, b) {}).onload = function (){
                    
                    $('.site_contents').html($('<div class="video_cont"> <video class="main_content" autoplay controls  controlsList="nodownload" style="display: block;"><source src="'+data.source+'" type="video/ogg"/><source src="'+data.source+'" /></video><div>'));
                }
                 
            }
        }

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

