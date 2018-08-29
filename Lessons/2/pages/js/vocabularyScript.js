
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
//   loadScript("../vocabularyData.js",function (a, b) {}).onload = function ()
//         {
//             init(pageNum);
//         };
   // }
//end key code 
}
    
//visible-xs visible-sm
function setGlossary(location){
//    debugger;
$(location).append('<div class="row gloss_data"><div class="col-xs-12 col-md-3"><div class="gloss_menu"><h1 class="block_title hidden-xs hidden-sm">الكلمة </h1><ul class="hidden-xs hidden-sm"></ul><!-- Split button --> <div class="btn-group dropdown visible-xs visible-sm">   <button type="button" class="btn dropdown_items">Action</button>   <button type="button" class=" btn dropdown-toggle dropdown_caret" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">     <span class="caret"></span>     <span class="sr-only">Toggle Dropdown</span>   </button>   <div class="clearfix"></div><ul class="dropdown-menu"></ul> </div></div></div><!--end col-xs-12--><div class="col-xs-12 col-md-9 "><div class="gloss_content"><div class="col-md-3 col-xs-6" onclick="showDefinition()"> <h1  class="block_title "> المرادف</h1></div> <div class="col-md-3 col-xs-6" onclick="showopposite()"><h1  class="block_title "> التضاد</h1>  </div> <div class="col-md-3 col-xs-6" onclick="showPlural()"><h1  class="block_title "> الجمع</h1></div><div class=" col-md-3 col-xs-6" onclick="showSingular()"><h1  class="block_title "> المفرد</h1></div><div class="clearfix"></div> <section><div class="row"><div class="col-md-3 col-xs-6"><article><p id="term_desc1">----</p></article></div><div class="col-md-3 col-xs-6"><article><p id="term_desc2">----</p></article></div><div class="col-md-3 col-xs-6"><article><p id="term_desc3">----</p></article></div><div class="col-md-3 col-xs-6"><article><p id="term_desc4">----</p></article></div><!--end col-xs-12 col-md-6 col-sm-6--><!--end row--></section></div></div><!--end col-xs-12--></div><!--end row-->');
    
}
function setGlossMenu(index){
//     debugger;
    var fristEnter=true;
    $('.gloss_menu ul').html('').scroll(function(){
        $(this).children("li").css({'visibility': 'visible', 'animation-name': 'flipInX'});
    }) ;
    if(index==0){
//         $('#term_desc1').html(terms[0].definition);
//        $('#term_desc2').html(terms[0].opposite);
//        $('#term_desc3').html(terms[0].plural);
//         $('#term_desc4').html(terms[0].singular);
         $( ".dropdown_items" ).html(terms[0].word);
        $.each(terms,function(num,w){    
            setDescWhenClickWord(num); 
        });
    }
    $('.gloss_menu ul a , .dropdown-menu > li > a').first().addClass('current_lesson');
    $('.dropdown-menu > li > a').first().addClass('current_lesson');
    $( ".gloss_menu ul" ).scrollTop( 0 );
    
}
function setDescWhenClickWord(num){
// debugger;
    $('<li ><a id="'+num+'">'+$.trim(terms[num].word)+'</a></li>').appendTo('.gloss_menu ul').click(function(){
       $('#term_desc1').html('----');
        $('#term_desc2').html('----');
        $('#term_desc3').html('----');
         $('#term_desc4').html('----');

        $('.gloss_menu').find('a').removeClass('current_lesson');
        $(this).find('a').addClass('current_lesson');
        $( ".dropdown_items" ).html(terms[num].word);
        //  console.log(this)
    }).addClass('wow flipInX');
    



}
function init(pageNum) {
    //terms is array of vocabulary
    terms=pages[pageNum].terms;
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    //function exist in custom function
    setMainContainer("body");
    setMainContent('#main_container'); 
    //**********
    $(".contents").addClass('temp_glossary');

    setGlossary('.contents');
    setGlossMenu(0);
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
    }

}

function showDefinition()
{
    var id = $(".current_lesson").attr("id");
    if(terms[id].definition=='')
        {
           
            $('#term_desc1').html('لا يوجد ');
        }
    else{
        $('#term_desc1').html(terms[id].definition);
//         $('#term_desc1').css('display','block');
    }
    
    
}
function showopposite()
{
     var id = $(".current_lesson").attr("id");
    if(terms[id].opposite=='')
        {
         $('#term_desc2').html('لا يوجد ');
        }
    else
    {
       $('#term_desc2').html(terms[id].opposite);   
//       $('#term_desc2').css('display','block');
    }
  
}
function showPlural()
{
     var id = $(".current_lesson").attr("id");
    if(terms[id].plural=='')
        {
         $('#term_desc3').html('لا يوجد ');  
        }
    else
    {
         $('#term_desc3').html(terms[id].plural);
//          $('#term_desc3').css('display','block');
    }
   
}

function showSingular()
{
     var id = $(".current_lesson").attr("id");
    if(terms[id].singular=='')
        {
          $('#term_desc4').html('لا يوجد ');
        }
    else{
           $('#term_desc4').html(terms[id].singular);
//          $('#term_desc4').css('display','block'); 
    }
 
}
    