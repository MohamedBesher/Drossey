//var  = getIDfromLocalStorage();
var json_array;
var itemsDetails_length,popOverTitle=" معلومات ";
var text_array = new Array();
var coords_array = new Array();
var transitions = ["flip","flow","turn","fade","pop","slideup","slidefade","slide","slidedown"];
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
function start(pageNum) {
    // this key code to link with player and load json 
   /* loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {
        */    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
        };
   // }
//end key code 
}
function init(pageNum) {
    $('title').text(hotspot[pageNum][0].name);  
    if(hotspot[pageNum][0].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(hotspot[pageNum][0].title);
    }
    setMainContainer("body");
    setMainContent('#main_container');
    $(".contents").addClass('temp_hotspot_click_reveal');
    if(hotspot[pageNum][0].article!=undefined){
        setParagraph(".contents");
        if(hotspot[pageNum][0].article.title!=undefined ){  
            $('.page_title').html(hotspot[pageNum][0].article.title);
        }else{$('.page_title').remove();}
        $('.page_desc').html(hotspot[pageNum][0].article.text);
    }
    setLMapROW('.contents');
    $('.circle').show(); 
    popOverTitle=hotspot[pageNum][0].pop_over_title;
    loadMaps();
    ////////////
    $('.carousel').carousel({
        interval: false
	});
	///////////
    $('.hotspot_pop').popover();
    if(hotspot[pageNum][0].tip_msg!=undefined){
        setNotiMsg("#main_container",hotspot[pageNum][0].tip_msg);
    }
    if(hotspot[pageNum][0].narration!=undefined )
    {
        if(hotspot[pageNum][0].narration!="")
        {
            playTapAudio(hotspot[pageNum][0].narration);
        } 
          else
      {
          $("#soundPlayer").empty();
      }
    
    }  
      else
      {
          $("#soundPlayer").empty();
      }
    
};
function loadPopups(_link){
    shuffleArray(transitions);
    var x_coords;
    var y_coords;
        $.each(text_array,function(tIndex,text){
            if(_link.id == tIndex){
                $("#dialog_text").text(text_array[tIndex]);
                x_coords = coords_array[tIndex].position.x;
                y_coords = coords_array[tIndex].position.y;
            }
        });
        $("#popupCloseRight").popup("open", {  
                x: x_coords,
                y: y_coords,
                transition:transitions[0],
                positionTo: "origin"
        });
    
}
function loadMaps(){
    if(hotspot[0]){
        json_array = hotspot[0];
        displayMaps(json_array);
    }
}
function displayMaps(maps_array){
    //width='"+maps_array[0].width+"' height='"+maps_array[0].height+"'
    $("#div_content").append("<img style='"+maps_array[0].style+"'  src='"+maps_array[0].main_img_src+"'  alt='map' usemap='#map'><map name='map' id='map'></map>") ;
var itemsDetails = maps_array[0].items_data,place;
    itemsDetails_length = itemsDetails.length;
    $.each(itemsDetails,function(index,item){
        place="top";
        text_array.push(item.text);
        var coords = item.position.x+","+item.position.y+",8";
 		var d_text = item.text ;
        var top = item.position.y+5  ;
        var left = item.position.x ;
        var coordObj = {x:left , y:top};
        coords_array.push(coordObj);
        
        $("#map").append("<area shape='circle' coords='"+coords+"' href='#'>");
        if(top<30 ){
            place="bottom";
        }
        $("#markers").append("<a tabindex='0' class='hotspot_pop' data-placement='"+place+"' style=\"top: "+top+"%; left: "+left+"%;\"  data-toggle='popover' data-trigger='focus' title='"+popOverTitle+"' data-content='"+d_text+"' onclick='loadAudio("+index+")'></a>");
    });
}
/*function getIDfromLocalStorage(){
  if (typeof(Storage) != "undefined") 
          {
                       var lesson_id = localStorage.getItem("lesson_id");
                     if(lesson_id){
                         return lesson_id;  
                     }else{
                      return null;
                     }
          }else{
            alert("Sorry, your browser does not support Web Storage...");
          }
}*/
function shuffleArray(array) {
    for (var i = array.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        var temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    return array;
}
function setLMapROW(location){
   $(location).append('<div class="map_content"><div class="row"><div class="col-xs-10 col-xs-offset-1 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2"><div id="div_content"></div><div id="markers"></div></div><!--end col--></div><!--end row--></div><!--end map_content-->');
}
function loadAudio(id)
{
     //play tab narration
                if(hotspot[0][0].items_data[id].sub_narration != undefined && hotspot[0][0].items_data[id].sub_narration != ''){
                 if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause();}
                    
                   playTapAudio(hotspot[0][0].items_data[id].sub_narration);
                    
                  

                }
                else{
                      if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause();}
                   // playAudio(clickSoundPath);
                }
}