var audioElement = document.createElement('audio');
var currentLevel = 1;
var score = 0;
var secSplit;
var sec;
var quesArray=[];
var quesArray2=[];
var position = 1;
var fire_click_flag ;
var questionsArray=[];

function init() {
    $('.top_title').html(quiz_body.header_arb);
	///call time function///
    for(var qqq=0;qqq<cannon_tank.length;qqq++){
        questionsArray[questionsArray.length]=qqq;
    }
    
	gameCountDown();	
    buildLevel(1);	
	initEvents();    
}
function buildLevel(level_no) {
	SD = 0;
	VD = 0;
	window.clearTimeout(SD);
	window.clearTimeout(VD);
    $(".fire_button").css("background","url(images/space-key5.png) no-repeat center");	
    	$('.timer').html("00:15");
        var q = "";
        questionsArray.sort(function () {
            return (Math.round(Math.random()) - 0.5)
        });
        for(var i=0 ; i<questionsArray.length ; i++){
            quesArray.push(questionsArray[i]);
        }
        questionsTxt =eval("cannon_tank["+quesArray[level_no-1]+"].questionsTxt");
        answersArray = eval("cannon_tank["+quesArray[level_no-1]+"].answers");  
        answersArray.sort(function () {
            return (Math.round(Math.random()) - 0.5)
        });		
        $('.quest').html(questionsTxt);	

    secSplit= $('.timer').text();
	sec=secSplit.substring($('.timer').text().lastIndexOf(":")+1);
	var projectile_top , projectile_left;
	$('.fire_cannon_container').hide();
	$('.fire_tank_container').hide();
	$('.tanks_container').animate({
    },2000,function(){
        fire_click_flag = 0;
        window.setTimeout(countDown,1000);			
        $(".fire_button").css('cursor','pointer');
        if (position == 1) {
            $(".right_button").css('cursor','pointer');
        } else if  (position == 2) {
            $(".left_button").css('cursor','pointer');
            $(".right_button").css('cursor','pointer');
        } else if  (position == 3) {
            $(".left_button").css('cursor','pointer');
            $(".right_button").css('cursor','pointer');
        } else if  (position == 4) {
            $(".left_button").css('cursor','pointer');
        }
    });
    projectile_top = ($('#cannon_parts_'+position).offset().top) - ($('.level').offset().top) - ($('.projectile').height()/2) - 4;
    if (position < 3 )
        projectile_left = ($('#cannon_parts_'+position).offset().left) - ($('.projectile').width()/2) + 6;
    else 
        projectile_left = ($('#cannon_parts_'+position).offset().left + $('#cannon_parts_'+position).width()) - ($('.projectile').width()/2) + 6;
		$('.projectile').css({'top': projectile_top , 'left': projectile_left});
    
    $('.projectile').hide();
	for (var i = 0 ; i<answersArray.length ; i++) {
		$('#tank_'+(i+1)).attr('trueAnswer',answersArray[i].trueAnswer);
		$('#answer_'+(i+1)).html(answersArray[i].answerTxt);
	}	
}

function initEvents(){	
	var projectile_top , projectile_left;
	$(".left_button").css('cursor','pointer');	
	$(".fire_button").click(function(){
		if (fire_click_flag == 0) {
			$('.projectile').show();
			$(".fire_button").css('cursor','default');
			$(".left_button").css('cursor','default');
			$(".right_button").css('cursor','default');
			fire_click_flag = 1;			
			projectile_top = ($('#tank_'+position).offset().top + $('#tank_'+position).height()) - ($('.level').offset().top) - 19;
			projectile_left = $('#tank_'+position).offset().left + ($('#tank_'+position).width()/2) - ($('.projectile').width()/2);			
			window.clearTimeout(SD);
			window.clearTimeout(VD);			
			$('.projectile').animate({
				top:  projectile_top,
				left: projectile_left
            },2000,function(){
				window.clearTimeout(SD);
				window.clearTimeout(VD);					
				$('.projectile').hide();
				if ($('#tank_'+position).attr("trueanswer") == "true") {
				    score++;						
//				    audio = document.createElement("audio");
				    audioElement.setAttribute('src','sound/CorrectAnswer.mp3');
				    audioElement.play();	
                    $('#answer_'+position).css('opacity','0');
                    $('#tank_'+position).removeClass('tank');
                    $('#tank_'+position).addClass('killed');
				    window.setTimeout(function(){
				        if (currentLevel < cannon_tank.length) {
                            $('#answer_'+position).css('opacity','1');
                            $('#tank_'+position).removeClass('killed');
                            $('#tank_'+position).addClass('tank');
				            currentLevel++;
				            buildLevel(currentLevel);
				        } else {
				            window.clearTimeout(finalTime);
				            finalResult(totaltime,score);	
				        }
				    },2000);
                } else {	
//				    audio = document.createElement("audio");
				    audioElement.setAttribute('src','sound/WrongAnswer.mp3');
				    audioElement.play();
                    $('.projectile').addClass('projectile2');
                    $('.projectile2').removeClass('projectile');
                    
				    $('.projectile2').css('top',  (parseInt($('.projectile2').css('top'))-30)+"px" ) ;
				    if(position == 4)
                        $('.projectile2').css('left',  (parseInt($('.projectile2').css('left')))+"px" ) ;
				    else if (position == 3)
				        $('.projectile2').css('left',  (parseInt($('.projectile2').css('left'))-3)+"px" ) ;
				    else	
				        $('.projectile2').css('left',  (parseInt($('.projectile2').css('left'))+10)+"px" ) ;
						projectile_top = $('.down_cannon').offset().top - $('.level').offset().top;
						projectile_left = $('.down_cannon').offset().left   +   ($('.down_cannon').width()/2);
						
						window.setTimeout(function(){
							$('.projectile2').show();
							$('.projectile2').animate({
								top:  projectile_top,
								left: projectile_left
				            },2000,function(){
								$('.projectile2').hide();
                                $('#cannon_'+position).removeClass('ca'+position);
                                $('#cannon_'+position).addClass('fr');
								window.setTimeout(function(){
								    if (currentLevel < cannon_tank.length) {
                                        $('.projectile2').addClass('projectile');
                                        $('.projectile2').removeClass('projectile2');
                                        $('#cannon_'+position).removeClass('fr');
                                        $('#cannon_'+position).addClass('ca'+position);
								        currentLevel++;
								        buildLevel(currentLevel);
								    } else {
								        window.clearTimeout(finalTime);
								        finalResult(totaltime,score);	
								    }
								},2000);
				            });
						},500);
                }
            });
        }
    });
	
	$(".right_button").click(function(){
		if (fire_click_flag == 0) {
			if (position < 4) {
				if(position == 1)
					$(".left_button").css('cursor','pointer');				
				    $('#cannon_parts_'+position).hide();
				    position++;
				    $('#cannon_parts_'+position).show();				
				    projectile_top = ($('#cannon_parts_'+position).offset().top) - ($('.level').offset().top) - ($('.projectile').height()/2) - 4;
				    if (position == 1)	
					   projectile_left = ($('#cannon_parts_'+position).offset().left) - ($('.projectile').width()/2) + 6;
				    else if (position == 2)
					   projectile_left = ($('#cannon_parts_'+position).offset().left) - ($('.projectile').width()/2) + 9;
				    else if (position == 3)
					   projectile_left = ($('#cannon_parts_'+position).offset().left + $('#cannon_parts_'+position).width()) - ($('.projectile').width()/2) - 14;
				    else
					   projectile_left = ($('#cannon_parts_'+position).offset().left + $('#cannon_parts_'+position).width()) - ($('.projectile').width()/2) - 7;				
				    $('.projectile').css({'top': projectile_top , 'left': projectile_left});
				    if (position == 4)
					   $(".right_button").css('cursor','pointer');
            }
        }
    });
	$(".left_button").click(function(){
		if (fire_click_flag == 0) {
			if (position > 1) {
				if(position == 4)
					$(".right_button").css('cursor','pointer');				
				$('#cannon_parts_'+position).hide();
				position--;
				$('#cannon_parts_'+position).show();				
				projectile_top = ($('#cannon_parts_'+position).offset().top) - ($('.level').offset().top) - ($('.projectile').height()/2) - 4;
				if (position == 1)	
					projectile_left = ($('#cannon_parts_'+position).offset().left) - ($('.projectile').width()/2) + 7;
				else if (position == 2)
					projectile_left = ($('#cannon_parts_'+position).offset().left) - ($('.projectile').width()/2) + 9;
				else if (position == 3)
					projectile_left = ($('#cannon_parts_'+position).offset().left + $('#cannon_parts_'+position).width()) - ($('.projectile').width()/2) - 13;
				else
					projectile_left = ($('#cannon_parts_'+position).offset().left + $('#cannon_parts_'+position).width()) - ($('.projectile').width()/2) - 5;
				$('.projectile').css({'top': projectile_top , 'left': projectile_left});
				if (position == 1)
					$(".left_button").css('cursor','pointer');
            }
        }
    });
	
	///function for information button ///
    $("#info").click(function(){
        $(".game_info").css("display","block");
//        $("body").animate({ scrollTop: 0 });
//        $("body").css({"overflow": "hidden"});
        window.scrollTo(0,0);
        $(".exit_info").click(function(){
            $(".game_info").css("display","none");
            $("body").css({"overflow": "auto"});
         });
    });
	
$(".timer_counter").css('visibility','hidden');
    ///reload button///
    $(".refresh_page").click(function(){
		window.location.reload();
    });
}

///function for time of each level///
function countDown() {    
        if(sec == 15) {
			VD=window.setTimeout(function(){				
				$('.total_score').hide();				
				$(".timer").html("00:00");
				window.clearTimeout(SD);
				window.clearTimeout(VD);			
				$('.game_info').hide();
				if (currentLevel < cannon_tank.length) {
					currentLevel++;
					setTimeout(function(){
						buildLevel(currentLevel);
					},2000);
				} else {				
					window.clearTimeout(finalTime);
					setTimeout(function(){
						finalResult(totaltime,score);						
					},2000);
				}
			},15000);
        }
    sec--;
    if(sec<=9){
        sec='0'+sec;
    }
    time="00:"+sec;
    $(".timer").html(time);
    SD=window.setTimeout(countDown, 1000);
}

/// function for time of game///
var totaltime=0;
function gameCountDown() {
	finalTime=window.setInterval(function(){
		totaltime++;
		if(totaltime<=9){
			totaltime="0"+totaltime;
			time="00:"+totaltime;
		} else if(totaltime>9  && totaltime <60){
			time = "00:"+totaltime;
		} else {
			var tmp = Math.round(((totaltime/60)-Math.floor(totaltime/60))*60);
			if (totaltime < 70)
				time = "01:0"+tmp;
			else {	
				var tmp2 = Math.floor(totaltime/60);
				if (tmp2<10)
					time = "0"+tmp2;
				else
					time = tmp2;				
				if (tmp == 60)
					time += ":00";
				else if (tmp <10)
					time += ":0"+tmp;
				else
					time += ":"+tmp;
            }
        }		
		$(".counter").html(time);	 
        if ( totaltime == '00') { 
			totaltime == '00'
			time="00:"+totaltime;
			$(".counter").html(time);
		}		
	},1000)
}

/// function for show final result of game///
function finalResult(secondCount,counts){
	finalScore(counts);	
	$("#score").unbind('click');
	$("#score i").css('cursor','default');
	$("#info").unbind('click');
	$("#info i").css('cursor','default');    
    $("body").animate({ scrollTop: 0 });
    $("body").css({"overflow": "hidden"});
     window.scrollTo(0,0);
    $(".game_result").css("display","block");
	$(".result_box").css("display","table");
	$(".established_time").html('( ثانية '+$('.counter').html()+' دقيقة)');
	$(".result_rate").html("("+counts+"/"+cannon_tank.length+")"); 
}

///function to show score for final result of game///
function finalScore(star){
    var quiz_nums=cannon_tank.length;
    var score_res=score*5/quiz_nums;    
    score_res=""+score_res+"";
    if(score_res.length != 1){
        score_res=score_res.charAt(0)+score_res.charAt(1)+score_res.charAt(2);
    }
    
    if(score_res==0 || score_res==0.1 || score_res==0.2 || score_res==0.3 || score_res==0.4 || score_res==0.5){
        $('#star1').addClass('win_half');
    }
    else if(score_res==0.6 || score_res==0.7 || score_res==0.8 || score_res==0.9 ||score_res==1){
        $('#star1').addClass('win');        
    }
    else if(score_res==1.1 || score_res==1.2 || score_res==1.3 || score_res==1.4 || score_res==1.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win_half');        
    }
    else if(score_res==1.6 || score_res==1.7 || score_res==1.8 || score_res==1.9 || score_res==2){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
    }
    else if(score_res==2.1 || score_res==2.2 || score_res==2.3 || score_res==2.4 || score_res==2.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win_half');
    }
    else if(score_res==2.6 || score_res==2.7 || score_res==2.8 || score_res==2.9 || score_res==3){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
    }
    else if(score_res==3.1 || score_res==3.2 || score_res==3.3 || score_res==3.4 || score_res==3.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win_half');        
    }
    else if(score_res==3.6 || score_res==3.7 || score_res==3.8 || score_res==3.9 || score_res==4){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
    }
    else if(score_res==4.1 || score_res==4.2 || score_res==4.3 || score_res==4.4 || score_res==4.5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
        $('#star5').addClass('win_half');        
    }
    else if(score_res==4.6 || score_res==4.7 || score_res==4.8 || score_res==4.9 || score_res==5){
        $('#star1').addClass('win');
        $('#star2').addClass('win');
        $('#star3').addClass('win');
        $('#star4').addClass('win');
        $('#star5').addClass('win');
    }
}