//var lessonID = getIDfromLocalStorage();
//var json_array;
var details = [];
var items = [];
var pageNum,flag=false;
//var text_array = new Array();
//var coords_array = new Array();
//var transitions = ["flip","flow","turn","fade","pop","slideup","slidefade","slide","slidedown"];
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
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
     var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
  loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
        };
  //  }
//end key code 
    }
function displayMaps(maps_array){
    
    $("#div_content").append("<img src='"+maps_array.main_img_src+"'   alt='map' usemap='#map'><map name='map' id='map'></map>") ;

}
function setWrapperRow(location) {
    
    // appended the slider structure 
    $(location).append(' <div class="hidden-xs hidden-sm col-md-2 "><div id="hotspot_slider" class="carousel slide" data-interval="0" data-ride="carousel"><!-- Wrapper for slides --><div class="carousel-inner" role="listbox"></div><!--end carousel-inner--><!-- Controls --><div class="hotspot_slider_control"><a class="right carousel-control" href="#hotspot_slider" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a><a class="left carousel-control" href="#hotspot_slider" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a></div></div><!--end hotspot_slider--></div><!--end col-xs-12-->');
        
}
function displayWrapper(){
    var curItem = 0;
    if(details.length<=6){$('.hotspot_slider_control').hide();}
    
    //appended items 
    for (var i =0;i<details.length;i+=6){
        curItem++
        $('<div class="item "><div class="row"></div><!--end row--></div><!--end item-- >').appendTo('.carousel-inner')
        for (var j =i;j<i+6 && j<details.length;j++){
            $('<div class="col-xs-12"><a  class="wow bounceIn hotspot_slider_btn ">'+details[j].title+'</a></div><!--end col-xs-12-->').appendTo('.carousel-inner .item:nth-child('+curItem+') .row');
        }
        
        
    }
    // to make the frist item and hotspot are active 
$('.carousel-inner .item:nth-child(1) ').addClass('active').find('a').first().addClass(' curr_hotspot');
    //this code to  loadpoup when click item 
    
    
    $( ".item .row div" ).click(function() {
  // `this` is the DOM element that was clicked
  var index = $( ".carousel-inner .item .row div" ).index( this );
           loadPopups(details[index]);
        $('.item div a').removeClass('curr_hotspot')
        $(this).find('a').addClass('curr_hotspot')
});
      //console.log(details)
    $.each(details,function(index,value){
        $('<li><a >'+value.title+'</a></li>').appendTo('.dropdown-menu').click(function() {
            // `this` is the DOM element that was clicked
           
            loadPopups(value);
            $('#dropdownMenu').html(value.title+'<span class="fa fa-caret-down"></span>')
            $('.dropdown-menu li a').removeClass('curr_hotspot')
            $('.dropdown-menu li a').eq(index).addClass('curr_hotspot')
        })
    })
    $('#dropdownMenu').html(details[0].title+'<span class="fa fa-caret-down"></span>')
    $('.dropdown-menu li a').eq(0).addClass('curr_hotspot')
    
    //////////////// visible-sm hidden-sm
    // to load frist popup
loadPopups(details[0]);
}
function setHotspotPanal(location){
    $(location).append('<div class="col-xs-4   col-md-3 "><div class="hotspot_details_panel"><h1 class="block_title hidden-sm hidden-xs">تفاصيل الخريطة 1</h1><div class="dropdown visible-xs visible-sm ">   <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">     تفاصيل الخريطة     <span class="caret"></span>   </button>   <ul class="dropdown-menu" aria-labelledby="dropdownMenu">                 </ul> </div><div class="hotspot_zoomin text-center"><div style="background-image:url(pics/hotspot/africa-map.jpg);background-position:-430px 790px;width:100%;;height:200px;"></div></div><!--end hotspot_zoomin--><div class=" hidden-sm hidden-xs "><div class=" hotspot_text text-justify small_desc"></div></div></div><!--end hotspot_text--></div><!--end hotspot_details_panel--></div><!--end col-xs-12--><div class=" visible-xs visible-sm col-xs-12 text-justify larg_desc"><div class=" hotspot_text text-justify"></div></div><!--end hotspot_text-->')
}
function loadPopups(item){
   
    var coords = item.position.x+","+item.position.y+",8";
    var top = item.position.y  ;
    var left = item.position.x ;
    var coordObj = {x:left , y:top};
    $("#map").html("<area shape='circle' coords='"+coords+"' href='#'>");
    $("#markers").html("<a tabindex='0' class='hotspot_pop' data-placement='top' style=\"top: calc( "+top+"% - 32px); left: calc( "+left+"% - 16px);\"   ></a>");
    $('.hotspot_details_panel h1').html(item.title)
    $('.hotspot_text').html(item.text)
    var imgItem=item.sub_img_src;
    if(imgItem!=undefined && imgItem!=""){
        $('.hotspot_zoomin').html('<img src="'+imgItem+'"/>')
    }else{
        $('.hotspot_zoomin').html('<div></div>')
        $('.hotspot_zoomin div').attr('style','background-image:url('+pages[pageNum].main_img_src+');background-position:'+left+'% '+top+'%;width:100%;;height:200px;')
    }
    if(flag){
        playAudio(dropSoundPath)
        if(item.sub_narration!=undefined){
            //subort sound
            if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
            playTapAudio(item.sub_narration)
        } 
    }
    flag= true
}
function init(pn) {
    pageNum=pn
    details=pages[pageNum].details;
    ////////////////////////////////
    $('title').text(pages[pageNum].name);  
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    $("#main_container").addClass('temp_hotspot_click_tabs')
    if(pages[pageNum].article!=undefined){
        setParagraph("#main_container")
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);
        }else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
    }
    $('#main_container').append('<div id="hotspot" class="row"></div><!--end row-->');
    setWrapperRow('#hotspot');
    setMMapROW('#hotspot');
    setHotspotPanal('#hotspot');
    $('.circle').show(); 
    displayWrapper();
    displayMaps(pages[pageNum]);
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
    }
    /*if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    }*/
    if(pages[pageNum].narration!=undefined && pages[pageNum].narration!=""){
       
       playTapAudio(pages[pageNum].narration)
       audioTap.bind('ended',function(){
            playTapAudio(details[0].sub_narration) 
        })}else{
            playTapAudio(details[0].sub_narration) 
       }
    
};
function setMMapROW(location){
   $(location).append('<div class="col-xs-8 col-md-7 col-sm-8"><div class="map_content"><div id="div_content"></div><div id="markers"></div></div><!--end map_content--></div><!--end col-xs-12-->')
}