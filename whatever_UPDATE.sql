UPDATE business
SET num_checkins = ch.valsum
FROM(
	SELECT business_id_num, SUM(check_ins_num) As valsum
	FROM checkin
	GROUP BY checkin.business_id_num
) As ch
WHERE ch.business_id_num = business.business_id_num;


UPDATE business
SET review_count = rvw.valsum
FROM(
	SELECT business_id_num, COUNT(business_id_num) As valsum
	FROM writerespondreview
	GROUP BY writerespondreview.business_id_num
) As rvw
WHERE rvw.business_id_num = business.business_id_num;



UPDATE business
SET review_rating = (star_sum/rvw_sum)
FROM(
	SELECT rvw.business_id_num,COUNT(rvw.business_id_num) As rvw_sum, SUM(rvw.stars) As star_sum
	FROM
	(SELECT writerespondreview.business_id_num, review.review_id_num,review.stars
	FROM writerespondreview, review
	WHERE writerespondreview.review_id_num = review.review_id_num)As rvw
	GROUP BY rvw.business_id_num
) As table1
WHERE table1.business_id_num = business.business_id_num;





