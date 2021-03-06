var correctCards = 0;
var cardsCount,DG;
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
      /*  loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
      //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
      loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
             init(pageNum)
        };
   // }
//end key code 
    
    
    }
function init(pageNum) {
    loadHTMLTags(pageNum);
  // Reset the game
  correctCards = 0;
  $('.circle_num ul').html( '' );
  $('.section_items').html( '' );
 
  // Create the pile of shuffled cards
  var numbers = [ ];
//console.log(pages[pageNum].data_gallary[0].question)
    // define dg as data array for gallary from json 
    DG=pages[pageNum].data_quiz;
   // console.log(pages[pageNum].data_quiz[0].question.length)
    cardsCount=DG.length;
//    localStorage.setItem("cardsCount", DG.length);
    //console.log(DG.length)
    for (var i=0;i<DG.length;i++){
        numbers.push({x:i+1, y:DG[i].co_answer});
    }  
		
  numbers.sort( function() { return Math.random() - .5 } );
 
  for ( var i=0; i<numbers.length; i++ ) {
      
    $('<span class="panel_drag">' + numbers[i].y + '</span>').data( 'number', numbers[i].x ).appendTo( '.panel_list' ).draggable( {
      containment: '#main_container',
         start:function(){ 
                    playAudio(dragSoundPath)
            },
        stack: '.panel_drag',
        cursor: 'move',
        revert: true
    } );
  }                                 
                                
  // Create the card slots
	
  for ( var i=1; i<=DG.length; i++ ) {
    $('<div class="drag_text "><div class="row"><div class="col-md-6 col-xs-12"><p class="drag_ques">' + DG[i-1].question + '</p></div><div class="col-md-6 col-xs-12"><div class="empty_drag"></div></div></div></div>').data( 'number', i ).appendTo( '.section_items' ).droppable( {
      accept: '.panel_drag',
      hoverClass: 'hovered',
      drop: handleCardDrop
    } );
  }
 
}
 //====================================
function handleCardDrop( event, ui ) {
   var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );
  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again
 
  if ( slotNumber == cardNumber ) {
//    ui.draggable.addClass( 'correct' );
//    ui.draggable.draggable( 'disable' );
//    $(this).droppable( 'disable' );
//    ui.draggable.position( { of: $(this).children('.row').children('.col-md-6').children('.empty_drag'), my: 'left top', at: 'left top' } );
     $(this).find('.empty_drag').text($(ui.draggable).text());
      $(ui.draggable).remove();
      $(this).find('.empty_drag').addClass('panal_desc').removeClass('empty_drag');
    //ui.draggable.draggable( 'option', 'revert', false );
    correctCards++;
     playAudio(dropSoundPath);
      if(DG[cardNumber-1].sub_narration!=undefined){
               //subort sound
               if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause();}
               playTapAudio(DG[cardNumber-1].sub_narration);
           }
      //playTapAudio(dropSoundPath)
  } 
   
   
  // If all the cards have been placed correctly then display a message
  // and reset the cards for another go
 
  if ( correctCards == cardsCount ) {
    $('#card_modal').modal('show');
      playAudio (cAnswerSoundPath);
  }
 
}
function loadHTMLTags(pageNum) {
     $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_19');
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents");
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove();}
        $('.page_desc').html(pages[pageNum].article.text);
        setCards(".page_desc");
        
    }else{
         setCards(".contents");
    }
    
    setCorDeclar();
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration);}    
    }   
    
}
function setCards(location) {
    $(location).after('<div class="col-md-8 col-sm-8 col-xs-8 section_items"></div><!--end col-md-7 col-xs-8--><div class="col-md-4 col-sm-4 col-xs-4 panel_list"></div><!--end col-md-4 col-xs-12-->');
  
     
}
function setCorDeclar(){
    $('.container').after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div class="alert alert-success" style="text-align:center;"> أحسنت إجابة صحيحة. </br>  تمت جميع الإختيارات بنجاح.   </div></div></div></div></div><!--/////////////////////////////-->');
    
}

