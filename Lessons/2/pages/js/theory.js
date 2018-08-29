
var terms=[];
var avalDigitsIds=[];
//to save avaliable letters id 
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
var setSlider = function(imgs) {
    var olTemp="", imgTemp="",className="active";
    $.each(imgs,function(index,value){
        olTemp+='<li class="'+className+'" data-target="#carousel2" data-slide-to="'+index+'"></li>';
        imgTemp+='<div class="item '+className+'"><img src="'+value.img+'" class="animated zoomIn" style="'+value.style+'"></div>';
        className=""
    })
    var all=('<div id="carousel2" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators">'+olTemp+'</ol><div class="carousel-inner" role="listbox">'+imgTemp+'</div><a class="left carousel-control" href="#carousel2" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel2" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>')
    return all
};
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
   // }
//end key code 
    
    
    }
function setGlossMenu(index){
    var fristEnter=true;
    $('.gloss_menu ul').html('') ;
    //console.log("word "+terms[0].word.charAt(0))
    if(index=='all_letters'){
        
    }else{
        $.each(terms,function(num,w){
            if(terms[num].word.charAt(0)==alphabit[index]){
                setDescWhenClickWord(num);
                if(fristEnter){
                $('#term_desc').html(terms[num].desc)
                $('#term_img').attr('src',terms[num].img_src)
                fristEnter=false;
                } 
            }
        })
    }
     $('.gloss_menu').find('a').first().addClass('current_lesson')
    
    
}
function setDescWhenClickWord(num){
    $('<li class="wow flipInX"><a href="#" >'+terms[num].word+'</a></li>').appendTo('.gloss_menu ul').click(function(){
        $('#term_desc').html(terms[num].desc)
        $('#term_img').attr('src',terms[num].img_src)
        $('.gloss_menu').find('a').removeClass('current_lesson')
        $(this).find('a').addClass('current_lesson')
  //  console.log(this)
                })
}
function init(pageNum) {
    terms=pages[pageNum].terms;
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_28')
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents")
        if(pages[pageNum].article.title!=undefined ){  
           $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
        
    }
    //set main tobic
    
    if(pages[pageNum].main_tobic!=undefined){
    $(' <div class="temp28_head"><h2>'+pages[pageNum].main_tobic.title+'</h2> <h4 class="animated bounceInDown">'+pages[pageNum].main_tobic.text+'</h4></div><!--end temp28_head-->').appendTo('.contents')
    }
    $('<div class="temp_28_tabs"><div class="row"> <div class="col-md-1 col-sm-2 col-xs-12"> <ul id="menu" class="nav nav-tabs " role="tablist"></ul> </div><!--end col-xs-12--><div class="col-md-11 col-sm-10 col-xs-12 temp28_tab_line"> <div id="desc" class="tab-content"></div><!--end tab-content--></div><!--end col-xs-12--></div><!--end row--></div><!--end temp_28_tabs-->').appendTo('.contents')
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
     if(pages[pageNum].narration!=undefined && pages[pageNum].narration!=""){
       playTapAudio(pages[pageNum].narration)
       audioTap.bind('ended',function(){
            if(terms[0].sub_narration != undefined && terms[0].sub_narration !="" ){ playTapAudio(terms[0].sub_narration) }
        })
     }else{
           if(terms[0].sub_narration != undefined && terms[0].sub_narration !=""){ playTapAudio(terms[0].sub_narration) }
       }
    loadTemplateBody()
  
}
function loadTemplateBody(){
    var setIMediaIsFound=function(value){
        var srt, media="",tempClass='class="just_desc"'; 
        if (value.desc != undefined && value.desc != "" ) {
            tempClass='class="with_desc"'
        }
        if( value.img != undefined  ){
            media='<div id="media_img"  '+tempClass+'><img src="'+value.img+'"  class="animated zoomIn" style="'+value.style+'" /></div>'
        }else if( value.video != undefined ){
             media='<div id="media_video" '+tempClass+'><video width="100%" controls preload="no" poster="'+value.video+'.png">                         <source type="video/mp4" src="'+value.video+'.mp4"></source>                           <source type="video/ogg" src="'+value.video+'.ogv"></source>                           <source type="video/webm" src="'+value.video+'.webm"></source>Your browser does not support HTML5 video.</video></div>'
        }else if( value.slider != undefined){
             media='<div id="media_slider" '+tempClass+'>'+setSlider(value.slider)+'</div>';
        } 
        
        if (value.desc != undefined && value.desc != "" && media != "") {
            srt = '<div class="col-xs-12 col-md-6 col-sm-6"><h4 class="animated bounceInDown">' + value.desc + '</h4></div><div class="col-xs-12 col-md-6 col-sm-6">' + media + '</div><div class="clearfix"> </div>'
        } else if (value.desc != undefined && value.desc != "" && media == "") {
            srt = '<h4 class="animated bounceInDown">' + value.desc + '</h4>'
        } else {
            srt = '<div class="col-xs-12 ">' + media + '</div><div class="clearfix"> </div>'
        }     
        return srt
    }
    //<h4 >'+value.desc+'</h4> margin: 0px 20px 20px; height: '+parseInt((60*terms.length)-35)+'px;
    $.each(terms,function(index,value){  
       $('<li ><a href="#t'+parseInt(index+1)+'" role="tab" data-toggle="tab">' +value.word +'</a></li>').appendTo('#menu').bind("click",function(){
           playAudio(clickSoundPath)
           if(value.sub_narration!=undefined){
               //support sound
               if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
               playTapAudio(value.sub_narration)
           }
           
       })
      // console.log(setIMediaIsFound(value))
      
       $('<div role="tabpanel" class="tab-pane fade  " id="t' + parseInt(index+1) + '">'+ setIMediaIsFound(value) +'</div><!--end tab-pane-->').appendTo('#desc')
       
    })
    $('#menu li').first().addClass("active")
    $('#desc div').first().addClass("in").addClass("active")
    
    
    
}