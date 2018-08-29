var currentPage;
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
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
        var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
             currentPage=pageNum
            loadHTMLTags(pageNum)
           
        };
    //}
//end key code 
    
    
    }
function loadHTMLTags(pageNum) {
    var gType, className;
    gType=pages[pageNum].gallary_type;
    className='';    
    if (gType=='vertical'){
        className='vertical_gallery';
    }
    $('title').text(pages[pageNum].name);  
    if(pages[pageNum].title!=undefined){
         setNewHeader("body");
         $('#lesson_title').text(pages[pageNum].title);
     }
    setMainContainer("body");
    setMainContent('#main_container');
    $(".contents").addClass('temp_14')
    setGallary(".contents",className)
    if(pages[pageNum].article!=undefined){
        if(pages[pageNum].article.title!=undefined ){        $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);}
    setGallery($('.gallery'), pages[pageNum].data_gallary,gType);
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    } 
}
function setGallery(location, dataG,gType) {
    location.html('<div id="carousel-images" class="carousel slide" data-ride="carousel"><!-- Indicators --><!-- Wrapper for slides --><div class="carousel-inner" role="listbox"></div><!--end carousel-inner--><ol class="carousel-indicators"></ol></div><!--end carousel-->');
    var innertHTML, className;
    for (var i=0;i<dataG.length;i++) {
          if (gType=='vertical'){
           // this code fpr vertical slide page 16
        innertHTML='<div class="row"><div class="col-md-3 col-xs-12"><img style="width:auto;" src="'+dataG[i].main_src+'" class="img_shadow" alt="..."></div><!--end col-md-9 col-xs-12--><div class="col-md-3 col-xs-12"><div class="slide_content"><h3 class="title_font">'+dataG[i].title+'</h3><p>'+dataG[i].desc+'</p></div><!--end slide_content--></div><!--end col-md-3 col-xs-12--><div class="clearfix"></div></div><!--end row-->'
        }else {
        // this code for normal slide in page 14
        innertHTML='<img src="'+dataG[i].main_src+'" alt="..."><div class="carousel-caption"><h2 class="title_font">'+dataG[i].title+'</h2><p>'+dataG[i].desc+'</p></div>'
        }
        //to set active div if i=0
        if(i==0){
            $('.carousel-inner').append('<div class="item active">'+innertHTML+'</div>');
        className='class="active"';
        }else{
            $('.carousel-inner').append('<div class="item">'+innertHTML+'</div><!--end item-->');
              className='';
             }
        //carousel html code 
 $('.carousel-indicators').append('<li data-target="#carousel-images" data-slide-to="'+i+'" '+className+'><a href="#"><img src="'+dataG[i].icon_src+'" alt=""/></a></li>')
 
    }
}
function setGallary(location,className){
    if(className==null){className=''}
    if(pages[currentPage].article!=undefined){$(location).append('<div class="row"><div class="col-md-12 col-sm-12 col-xs-12 "><h2 class="title_font page_title"></h2><p class="page_desc text-justify"></p> </div><!--end col-xs-12--><div class="col-md-12  col-sm-12  col-xs-12"><div class="gallery '+className+' "> </div><!--end gallery--></div><!--end col-xs-10--><!--end row-->')}else{ $(location).append(' <div class="row"><div class="col-xs-8 col-xs-offset-2"><div class="gallery '+className+' "> </div><!--end gallery--> </div></div>')}
   
}