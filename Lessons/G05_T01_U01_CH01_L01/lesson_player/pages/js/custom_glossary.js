var alphabit=["ا","ب","ت","ث","ج","ح","خ","د","ذ","ر","ز","س","ش","ص","ض","ط","ظ","ع","غ","ف","ق","ك","ل","م","ن","ه","لأ","و","ي"];
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
function start(pageNum) {
     
    // this key code to link with player and load json 
    /*loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a,    b) {}).onload = function () {*/
      var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
  loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
        };
   // }
//end key code 
}
    
function setGlossDigits(location){
    $('<span id="all_letters" class="wow zoomInRight digits_value active_digit">الكل</span>').appendTo('.gloss_digits').click(function(){
        setGlossMenu('all_letters'); 
        $.each(avalDigitsIds,function(num,id){
            $(id).removeClass('active_digit')
            $('#all_letters').addClass('active_digit')
        })
    })
    avalDigitsIds.push('#all_letters')
    $.each(alphabit,function(index,letter){
        $('<span id="letter'+index+'" class="wow zoomInRight digits_value">'+alphabit[index]+'</span>').appendTo('.gloss_digits')
        // code to make letter clickable
        var findWord=false; // check charchter is finding words to it or not
        $.each(terms,function(num,w){
            if($.trim(terms[num].word).charAt(0)==alphabit[index]){
                findWord=true;
                $('#letter'+index).click(function(){
                    setGlossMenu(index); 
                    avalDigitsIds.push('#letter'+index);
                    $.each(avalDigitsIds,function(num,id){
                        $(id).removeClass('active_digit')
                        $('#letter'+index).addClass('active_digit')
                    })
                    
                })
            }
    })
     
     if (!findWord){
        $('span[id=letter'+index+']').css('opacity', '0.5').css('cursor', 'auto').removeClass('hover'); 
         
     }
     //end 
   
    })

    
    
}//visible-xs visible-sm
function setGlossary(location){
    $(location).append('<div class="row"><div class="col-xs-12 col-md-10 col-md-offset-1"><div class="gloss_digits"></div><!--end gloss_digits--></div><!--end col-xs-12--></div><!--end row-->')
    $(location).append('<div class="row gloss_data"><div class="col-xs-12 col-md-4"><div class="gloss_menu"><h1 class="block_title hidden-xs hidden-sm">الكلمة </h1><ul class="hidden-xs hidden-sm"></ul><!-- Split button --> <div class="btn-group dropdown visible-xs visible-sm">   <button type="button" class="btn dropdown_items">Action</button>   <button type="button" class=" btn dropdown-toggle dropdown_caret" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">     <span class="caret"></span>     <span class="sr-only">Toggle Dropdown</span>   </button>   <div class="clearfix"></div><ul class="dropdown-menu"></ul> </div></div></div><!--end col-xs-12--><div class="col-xs-12 col-md-8 "><div class="gloss_content"><h1 class="block_title hidden-xs hidden-sm"> الاصطلاح</h1><section><div class="row"><div class="col-xs-12 col-md-6 col-sm-6"><article><p id="term_desc"></p></article></div><!--end col-xs-12 col-md-6 col-sm-6--><div class="col-xs-12 col-md-6 col-sm-6"><img id="term_img" src="../Data/Lessons/0/images/pics/atco.jpg" alt=""/></div><!--end col-xs-12 col-md-6 col-sm-6--></div><!--end row--></section></div></div><!--end col-xs-12--></div><!--end row-->')
    
}
function setGlossMenu(index){
    var fristEnter=true;
    $('.gloss_menu ul').html('').scroll(function(){
        $(this).children("li").css({'visibility': 'visible', 'animation-name': 'flipInX'})
    }) ;
    if(index=='all_letters'){
        $.each(terms,function(num,w){  
            if(fristEnter){
                $('.gloss_content section').html(setMedia(terms[num].desc,terms[num].media))
//                $('.all-media').css('height',''+$('.contents').height()*.6+'px')
                 $( ".dropdown_items" ).html(terms[num].word)

                fristEnter=false;
                }
            setDescWhenClickWord(num); 
        })
    }else{
        $.each(terms,function(num,w){
            if($.trim(terms[num].word).charAt(0)==alphabit[index]){
                setDescWhenClickWord(num);
                if(fristEnter){
                $('.gloss_content section').html(setMedia(terms[num].desc,terms[num].media))
//                $('.all-media').css('height',''+$('.contents').height()*.6+'px')
                fristEnter=false;
                    $( ".dropdown_items" ).html(terms[num].word)
                } 
            }
        })
    }
    $('.gloss_menu ul a , .dropdown-menu > li > a').first().addClass('current_lesson')
    $('.dropdown-menu > li > a').first().addClass('current_lesson')
    $( ".gloss_menu ul" ).scrollTop( 0 );
    
}
function setDescWhenClickWord(num){

    $('<li ><a>'+$.trim(terms[num].word)+'</a></li>').appendTo('.gloss_menu ul').click(function(){
        $('.gloss_content section').html(setMedia(terms[num].desc,terms[num].media))
//        $('.all-media').css('height',''+$('.contents').height()*.6+'px')
        $('.gloss_menu').find('a').removeClass('current_lesson')
        $(this).find('a').addClass('current_lesson')
        $( ".dropdown_items" ).html(terms[num].word)
        //  console.log(this)
    }).addClass('wow flipInX')
    



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
    $(".contents").addClass('temp_glossary')
    //setCardsDragROW('.contents');
    setGlossary('.contents');
    setGlossDigits('.contents');
    //setFinalDeclar('#main_container');
    setGlossMenu('all_letters');
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
    }

}
function setCardsDragROW(location){
   $(location).append('<div class="row"><div class="cards_content"><div id="cardPile" class="col-md-4" ></div><div class="col-md-4"></div><div id="cardSlots" class="col-md-4"></div><div class="clearfix"></div></div><!--end fliping_content--></div><!--end row-->')
}
/*function setMedia(text,mediaData){
    //console.log(mediaData)
    
    var  media="";
    if(mediaData != undefined){
    if( mediaData.img != undefined  && mediaData.img != ""){
        media='<div class="media_img"  ><img src="'+mediaData.img+'"  /></div>'
    }else if( mediaData.video != undefined  &&  mediaData.video!= "" ){
        media='<div class="media_video" ><video width="100%" controls preload="no" poster="'+mediaData.video+'.png">                         <source type="video/mp4" src="'+mediaData.video+'.mp4"></source>                           <source type="video/ogg" src="'+mediaData.video+'.ogv"></source>                           <source type="video/webm" src="'+mediaData.video+'.webm"></source>Your browser does not support HTML5 video.</video></div>'
    }else if( mediaData.slider != undefined   &&  mediaData.slider != "" ){
        sNum=1;//slider number for different id 
        var olTemp="", imgTemp="",className="active";
        $.each(mediaData.slider,function(index,value){
            olTemp+='<li class="'+className+'" data-target="#carousel'+sNum+'" data-slide-to="'+index+'"></li>';
            imgTemp+='<div class="item '+className+'"><img src="'+value+'"></div>';
            className=""
        })
        media='<div class="media_slider" ><div id="carousel'+sNum+'" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators">'+olTemp+'</ol><div class="carousel-inner" role="listbox">'+imgTemp+'</div><a class="left carousel-control" href="#carousel'+sNum+'" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel'+sNum+'" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div><!---end of carousel---></div>'
        sNum++
    } 
         }
    //class="media_with_text   just_media "
    if(text!=undefined && text!="" && media !=""){
        media='<div class="col-xs-12 col-md-6 col-sm-6"><div class="media_desc text-justify" >' + text + '</div></div><div class="col-xs-12 col-md-6 col-sm-6">' + media + '</div><div class="clearfix"> </div>'
    }else if(text!=undefined && text!="" && media ==""){
        media= '<div class="media_desc text-justify" >' + text + '</div><div class="clearfix"> </div>'
    }else{
        media='<div class="col-xs-12 ">' + media + '</div><div class="clearfix"> </div>'
    }
    media='<div class="row">'+media+'</div><!--  end media row  -->'
    return media
   
} */ 
    